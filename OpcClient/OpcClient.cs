using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using CyScada.Model;
using IOClient;

namespace CyScada.Web.OpcClient
{
    public static class OpcClient
    {
        private static ISmartClient _Client = new SmartClient();

        public static Hashtable ItemTable = new Hashtable();

        private static Thread opcThread;
        private static bool _IsRun = false;
        private static SmartGroup sg;
        public static ISmartClient Client
        {
            get { return _Client; }
            set { _Client = value; }
        }


        public static void StopClient()
        {
            _IsRun = false;
            Thread.Sleep(1000);
        }



        public static void StartClient()
        {
            _Client.Connect("127.0.0.1", "8081", "IODriver");
            _Client.Groups.Add("test", 1000);
            var items = _Client.BrownseAllTags();
            foreach (var smartItem in items)
            {
                ItemTable.Add(smartItem.SmartItemKey, new TagItemModel
                {
                    Desc = smartItem.Description,
                    Key = smartItem.SmartItemKey,
                    Name = smartItem.Name,
                    Quality = smartItem.Quality,
                    TimeStamp = smartItem.TimeStamp,
                    Value = smartItem.Value,
                    ValueIsChanged = smartItem.IsChanged,
                    ValueType = smartItem.ValueType
                });
                _Client.Groups[0].Items.Add(smartItem.SmartItemKey);
            }


            opcThread = new Thread(start =>
            {
                while (_IsRun)
                {
                    try
                    {
                        for (var i = 0; i < _Client.Groups[0].Items.Count; i++)
                        {
                            var key = _Client.Groups[0].Items[i].SmartItemKey;
                            var tag = ItemTable[key] as TagItemModel;
                            tag.Value = _Client.Groups[0].Items[i].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    Thread.Sleep(1000);
                }
            });
            _IsRun = true;
            opcThread.Start();
        }


        public static void SetValue(string key, object value)
        {
            for (int i = 0; i < _Client.Groups[0].Items.Count; i++)
            {
                if (_Client.Groups[0].Items[i].SmartItemKey == key)
                {
                    _Client.Groups[0].Items.SetSItemValue(key, value);
                }
            }



        }

    }
}