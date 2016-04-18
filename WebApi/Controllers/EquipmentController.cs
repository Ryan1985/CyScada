using System.Collections.Generic;
using System.Web.Http;
using CyScada.Model;
using CyScada.BLL;

namespace CyScada.Web.WebApi.Controllers
{
    [ApiAuth]
    public class EquipmentController : ApiController
    {
        // GET api/equipment
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/equipment/5
        public EquipmentModel Get(string equipmentId)
        {
            var equipment = new BllEquipment().GetEquipment(equipmentId);
            return equipment;
        }

        // POST api/equipment
        public void Post([FromBody]string value)
        {
        }

        // PUT api/equipment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/equipment/5
        public void Delete(int id)
        {
        }
    }
}
