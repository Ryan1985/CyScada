using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using CyScada.BLL;
using CyScada.Model;

namespace CyScada.Web.WebApi.Controllers
{
    public class ParameterSettingController : ApiController
    {

        private BllRealTimeDisplay _bllRealTimeDisplay = new BllRealTimeDisplay();

        public BllRealTimeDisplay BLLRealTimeDisplay
        {
            get { return _bllRealTimeDisplay; }
            set { _bllRealTimeDisplay = value; }
        }




        public IList<TagItemModel> Get(string sideMenuId, string userId)
        {
            var result = new List<TagItemModel>();
            for (int i = 0; i < 8; i++)
            {
                result.Add(new TagItemModel {
                    Key = "tcp://127.0.0.1:8081/IODriver/GKJ.GJKDevice.400"+(i+25).ToString()
                });
            }
            return result;
        }


        // GET api/parametersetting
        public Hashtable Get()
        {
            return OpcClient.OpcClient.ItemTable;
        }

        // GET api/parametersetting/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/parametersetting
        public void Post([FromBody] string[] values)
        {
            OpcClient.OpcClient.SetValue(values[0], values[1]);
        }

        // PUT api/parametersetting/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/parametersetting/5
        public void Delete(int id)
        {
        }
    }
}
