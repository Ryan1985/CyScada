using System.Collections.Generic;
using System.Web.Http;
using CyScada.Model;
using CyScada.BLL;
using Newtonsoft.Json;

namespace CyScada.Web.WebApi.Controllers
{
    [ApiAuth]
    public class EmployeeListController : ApiController
    {

        private BllEmployee _bllEmployee = new BllEmployee();

        public BllEmployee BLLEmployee
        {
            get { return _bllEmployee; }
            set { _bllEmployee = value; }
        }


        // GET api/employeelist
        public IEnumerable<EmployeeModel> Get()
        {
            return _bllEmployee.GetEmployeeList();
        }

        // GET api/employeelist/5
        public IEnumerable<EmployeeModel> Get(string paramstring)
        {
            var model = JsonConvert.DeserializeObject<EmployeeModel>(paramstring);
            return model==null ? _bllEmployee.GetEmployeeList() : _bllEmployee.GetEmployeeList(model);
        }

        // POST api/employeelist
        public void Post([FromBody]string value)
        {
        }

        // PUT api/employeelist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/employeelist/5
        public void Delete(int id)
        {

        }
    }
}
