using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CyScada.Model;
using CyScada.BLL;

namespace CyScada.Web.WebApi.Controllers
{
    [ApiAuth]
    public class EquipmentSelectionController : ApiController
    {

        // GET api/equipmentselection/5
        public IEnumerable<EquipmentModel> Get(string userId)
        {
            
            var equipmentList = new BllEquipmentSelection().GetEquipments(userId);
            return equipmentList;

        }

        // POST api/equipmentselection
        public void Post([FromBody]string value)
        {
        }

        // PUT api/equipmentselection/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/equipmentselection/5
        public void Delete(int id)
        {
        }
    }
}
