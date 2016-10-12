using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using CyScada.BLL;
using CyScada.Model;

namespace CyScada.Web.OpcClient
{
    public static class OpcClient
    {
        private static readonly IOClientOPC ClientList = new IOClientOPC();

        public static Hashtable ItemTable = new Hashtable();
        private static readonly object MLock = new object();
        private static readonly object QLock = new object();
        private static readonly object LLock = new object();
        private static Thread opcThread;
        private static bool _isRun = true;
        private static readonly Queue<object[,]> RecQueue = new Queue<object[,]>(10);
        private static readonly Queue<string> LogQueue = new Queue<string>(10);
        private static DataTable dtTags = null;


        private static string LogPath =
            Assembly.GetCallingAssembly().Location.Remove(Assembly.GetCallingAssembly().Location.LastIndexOf('\\'));
        //private static SmartGroup sg;
        //public static ISmartClient Client
        //{
        //    get { return _Client; }
        //    set { _Client = value; }
        //}


        public static void StopClient()
        {
            LogQueue.Enqueue("StopClient");
            ClientList.StopRead();
            ClientList.DisConnectServer();
            _isRun = false;
            Thread.Sleep(1000);
        }



        public static void StartClient()
        {
            LogPath = ConfigurationManager.AppSettings["LogAddress"];
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            LogQueue.Enqueue("StartClient");


            ThreadPool.QueueUserWorkItem(w =>
            {
                while (_isRun)
                {
                    if (LogQueue.Count > 0)
                    {
                        var log = LogQueue.Dequeue();
                        File.AppendAllText(LogPath + "\\Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt",
                            "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "]" + log + "\r\n");
                    }
                    Thread.Sleep(100);
                }
            });


            ThreadPool.QueueUserWorkItem(w =>
            {
                while (_isRun)
                {
                    try
                    {
                        if (dtTags == null)
                        {
                            var bllTags = new BllMachineTagList();
                            dtTags = bllTags.GetAllTags();
                        }
                        var serverAddress = ConfigurationManager.AppSettings["ServerAddress"];
                        var serverNameList =
                            dtTags.AsEnumerable().Select(dr => dr["ServerAddress"].ToString()).Distinct().ToList();
                        ClientList.EnqueueRec += client_EnqueueRec;
                        ClientList.EnqueueLog += client_EnqueueLog;
                        ClientList.ServerAddress = serverAddress + "/" + serverNameList.First();
                        ClientList.ConnectServer();
                        ClientList.AddItems(
                            dtTags.AsEnumerable().Select(dr => dr["DeviceName"].ToString()).Distinct().ToArray());
                        ClientList.StartRead();
                        break;
                    }
                    catch (Exception ex)
                    {
                        LogQueue.Enqueue("启动OPC模块发生错误:" + ex.Message);
                    }
                    Thread.Sleep(60000);
                }


            });


            ThreadPool.QueueUserWorkItem(w =>
            {
                while (_isRun)
                {
                    if (RecQueue.Count > 0)
                    {
                        lock (MLock)
                        {
                            var rec = RecQueue.Dequeue();
                            for (var i = 0; i < rec.Length / 5; i++)
                            {
                                var key = rec[i, 4].ToString();
                                if (ItemTable.ContainsKey(key))
                                {
                                    var tag = ItemTable[key] as TagItemModel;
                                    if (tag == null)
                                    {
                                        ItemTable[key] = new TagItemModel
                                        {
                                            Key = key,
                                            Name = rec[i, 0].ToString(),
                                            Value = rec[i, 1],
                                            TimeStamp = (DateTime)rec[i, 2],
                                            Quality = rec[i, 3].ToString()
                                        };
                                    }
                                    else
                                    {
                                        tag.Name = rec[i, 0].ToString();
                                        tag.Value = rec[i, 1];
                                        tag.TimeStamp = (DateTime)rec[i, 2];
                                        tag.Quality = rec[i, 3].ToString();
                                    }
                                }
                                else
                                {
                                    ItemTable.Add(key, new TagItemModel
                                    {
                                        Key = key,
                                        Name = rec[i, 0].ToString(),
                                        Value = rec[i, 1],
                                        TimeStamp = (DateTime)rec[i, 2],
                                        Quality = rec[i, 3].ToString()
                                    });
                                }
                            }
                        }
                    }
                    Thread.Sleep(100);
                }
            });


            //_Client.ConnectServer();


            //_Client.Connect("123.57.57.171", "8081", "IODriver");
            //_Client.Groups.Add("test", 1000);
            //var items = _Client.BrownseAllTags();
            //foreach (var smartItem in items)
            //{
            //    ItemTable.Add(smartItem.SmartItemKey, new TagItemModel
            //    {
            //        Desc = smartItem.Description,
            //        Key = smartItem.SmartItemKey,
            //        Name = smartItem.Name,
            //        Quality = smartItem.Quality,
            //        TimeStamp = smartItem.TimeStamp,
            //        Value = smartItem.Value,
            //        ValueIsChanged = smartItem.IsChanged,
            //        ValueType = smartItem.ValueType
            //    });
            //    _Client.Groups[0].Items.Add(smartItem.SmartItemKey);
            //}


            //opcThread = new Thread(start =>
            //{
            //    while (_IsRun)
            //    {
            //        try
            //        {
            //            for (var i = 0; i < _Client.Groups[0].Items.Count; i++)
            //            {
            //                var key = _Client.Groups[0].Items[i].SmartItemKey;
            //                var tag = ItemTable[key] as TagItemModel;
            //                tag.Value = _Client.Groups[0].Items[i].Value;
            //                tag.TimeStamp = _Client.Groups[0].Items[i].TimeStamp;
            //                tag.Quality = _Client.Groups[0].Items[i].Quality;
            //            }
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //        Thread.Sleep(1000);
            //    }
            //});
            //_IsRun = true;
            //opcThread.Start();
        }

        static void client_EnqueueLog(string LogString)
        {
            lock (LLock)
            {
                LogQueue.Enqueue(LogString);
            }
        }

        static void client_EnqueueRec(object[,] rec)
        {
            lock (QLock)
            {
                RecQueue.Enqueue(rec);
            }
        }


        public static void SetValue(string key, object value)
        {
            var itemName = key.Substring(key.LastIndexOf('/')+1, key.Length - key.LastIndexOf('/')-1);
            ClientList.WriteValue(itemName, string.Empty, value);
            //for (int i = 0; i < _Client.Groups[0].Items.Count; i++)
            //{
            //    if (_Client.Groups[0].Items[i].SmartItemKey == key)
            //    {
            //        _Client.Groups[0].Items.SetSItemValue(key, value);
            //    }
            //}
        }

    }
}