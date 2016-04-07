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
    public class EmployeeController : ApiController
    {
        private BllEmployee _bllEmployee = new BllEmployee();

        public BllEmployee BLLEmployee
        {
            get { return _bllEmployee; }
            set { _bllEmployee = value; }
        }


        // GET api/employee
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/employee/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/employee
        public string Post([FromBody]EmployeeModel model)
        {
            var result = _bllEmployee.SaveEmployee(model);
            return result;
        }

        // PUT api/employee/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/employee/5
        public void Delete(int id)
        {
            _bllEmployee.DeleteEmployee(id);
        }
    }
}
