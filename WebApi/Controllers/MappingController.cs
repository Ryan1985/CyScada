using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CyScada.BLL;
using CyScada.Model;

namespace CyScada.Web.WebApi.Controllers
{
    public class MappingController : ApiController
    {
        private BllMapping _bllMapping = new BllMapping();

        public BllMapping BLLMapping
        {
            get { return _bllMapping; }
            set { _bllMapping = value; }
        }


        // GET api/mapping
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        public DataTable Get(string mappingType,string paramString)
        {
            return _bllMapping.GetMappingList(mappingType, paramString);
        }



        // GET api/mapping/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/mapping
        public void Post([FromBody]MappingTableModel model)
        {
            _bllMapping.Save(model);
        }

        // PUT api/mapping/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/mapping/5
        public void Delete(int id)
        {
        }
    }
}
