using System.Collections.Generic;
using CyScada.Model;

namespace CyScada.BLL
{
    public class BllAuthorityAssign
    {
        private BllEmployee _bllEmployee = new BllEmployee();
        private BllRole _bllRole = new BllRole();


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

        public EmployeeModel GetEmployeeWithAuthority(string userId)
        {
            return _bllEmployee.GetEmployeeWithAuthority(userId);
        }

        public string AddUserRole(string userId, string roleId)
        {
            return _bllEmployee.AddEmployeeRole(userId, roleId);
        }

        public string AddUserAuthority(string userId, string authorityId)
        {
            return _bllEmployee.AddEmployeeAuthority(userId, authorityId);
        }

        public string DeleteUserRole(string userId, string roleId)
        {
            return _bllEmployee.DeleteEmployeeRole(userId, roleId);
        }

        public string DeleteUserAuthority(string userId, string authorityId)
        {
            return _bllEmployee.DeleteEmployeeAuthority(userId, authorityId);
        }


        public string Assign(AssignToggleModel model)
        {
            var methodName = model.ToggleType + model.ToggleHost;
            var method = GetType().GetMethod(methodName);
            if (method == null)
            {
                return "指令错误，无法找到["+methodName+"]的指令";
            }

            return method.Invoke(this, new object[] {model.UserId, model.Id}).ToString();

        }


    }
}
