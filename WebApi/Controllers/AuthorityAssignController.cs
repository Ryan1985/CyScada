using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CyScada.BLL;
using CyScada.Model;

namespace CyScada.Web.WebApi.Controllers
{
    
    public class AuthorityAssignController : ApiController
    {
        private BllAuthorityAssign _bllAuthorityAssign = new BllAuthorityAssign();
        private BllEmployee _bllEmployee = new BllEmployee();
        private BllRole _bllRole = new BllRole();


        public BllAuthorityAssign BLLAuthorityAssign
        {
            get { return _bllAuthorityAssign; }
            set { _bllAuthorityAssign = value; }
        }

        public BllEmployee BLLEmployee
        {
            get { return _bllEmployee; }
            set { _bllEmployee = value; }
        }

        public BllRole BLLRole
        {
            get { return _bllRole; }
            set { _bllRole = value; }
        }


        // GET api/authorityassign
        public IEnumerable<object> Get()
        {
            return new IEnumerable[] {_bllEmployee.GetEmployeeList(), _bllRole.GetRoleList()};
        }

        // GET api/authorityassign/5
        public EmployeeModel Get(string userId)
        {
            return _bllAuthorityAssign.GetEmployeeWithAuthority(userId);
        }

        // GET api/authorityassign/5
        public RoleModel Get(string userId,string roleId)
        {
            return _bllAuthorityAssign.GetRoleWithAuthority(roleId);
        }

        // POST api/authorityassign
        public string Post([FromBody]AssignToggleModel model)
        {
            return _bllAuthorityAssign.Assign(model);
        }

        // PUT api/authorityassign/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/authorityassign/5
        public void Delete(int id)
        {
        }
    }
}
