using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CyScada.DAL;
using CyScada.Model;
using CyScada.Common;

namespace CyScada.BLL
{
    public class BllEmployee
    {

        private DalEmployee _dalEmployee = new DalEmployee();
        private DalEmpRoles _dalEmpRoles = new DalEmpRoles();
        private DalRole _dalRole = new DalRole();
        private DalAuthority _dalAuthority = new DalAuthority();

        public DalEmployee DalEmployee
        {
            get { return _dalEmployee; }
            set { _dalEmployee = value; }
        }

        public DalEmpRoles DalEmpRoles
        {
            get { return _dalEmpRoles; }
            set { _dalEmpRoles = value; }
        }

        public DalRole DalRole
        {
            get { return _dalRole; }
            set { _dalRole = value; }
        }

        public DalAuthority DalAuthority
        {
            get { return _dalAuthority; }
            set { _dalAuthority = value; }
        }

        public IEnumerable<EmployeeModel> GetEmployeeList()
        {
            return GetEmployeeList(new EmployeeModel());
        }



        public IEnumerable<EmployeeModel> GetEmployeeList(EmployeeModel filterModel)
        {
            var dtEmployees = _dalEmployee.GetEmployeeList(filterModel.ToHashTable());
            return dtEmployees.AsEnumerable().Select(dr => new EmployeeModel
            {
                //Authority = Convert.ToInt32(dr["Authority"]),
                AuthorityCode=dr["AuthorityCode"].ToString(),
                Code = dr["Code"].ToString(),
                Description = dr["Description"].ToString(),
                Id = Convert.ToInt32(dr["Id"]),
                LoginName = dr["LoginName"].ToString(),
                Name = dr["Name"].ToString(),
                Password = CommonUtil.Decrypt(dr["Password"].ToString())
            });


        }

        public string SaveEmployee(EmployeeModel model)
        {
            model.Password = CommonUtil.Encrypt(model.Password);
            return model.Id.HasValue ? ModifyEmployee(model) : CreateEmployee(model);
        }

        protected string CreateEmployee(EmployeeModel model)
        {
            return _dalEmployee.CreateEmployee(model.ToHashTable());
        }

        protected string ModifyEmployee(EmployeeModel model)
        {
            return _dalEmployee.ModifyEmployee(model.ToHashTable());
        }



        public void DeleteEmployee(int id)
        {
            _dalEmployee.DeleteEmployee(id);
        }

        internal string AddEmployeeRole(string userId, string roleId)
        {
            return DalEmpRoles.CreateEmpRoles(new Hashtable
            {
                {"UserId",userId},
                {"RoleId",roleId}
            });
        }

        internal string AddEmployeeAuthority(string userId, string authorityCode)
        {
            //获取用户的信息
            var dtEmployee = _dalEmployee.GetEmployeeList(new Hashtable
            {
                {"ID", userId}
            });

            if (dtEmployee.Rows.Count == 0)
            {
                return "当前用户不存在！";
            }

            var employee = dtEmployee.AsEnumerable().Select(dr => new EmployeeModel
            {
                //Authority = Convert.ToInt32(dr["Authority"]) | Convert.ToInt32(authorityId),//使用或运算添加权限
                AuthorityCode = CommonUtil.AppendAuthorityCode(dr["AuthorityCode"].ToString(),authorityCode),
                Code=dr["Code"].ToString(),
                Description=dr["Description"].ToString(),
                Id=Convert.ToInt32(dr["Id"]),
                LoginName=dr["LoginName"].ToString(),
                Name=dr["Name"].ToString(),
                Password=dr["Password"].ToString()
            }).ToList().First();

           return _dalEmployee.ModifyEmployee(employee.ToHashTable());
        }

        internal string DeleteEmployeeRole(string userId, string roleId)
        {
            return DalEmpRoles.DeleteEmployee(new Hashtable
            {
                {"UserId",userId},
                {"RoleId",roleId}
            });
        }

        internal string DeleteEmployeeAuthority(string userId, string authorityCode)
        {

            //获取用户的信息
            var dtEmployee = _dalEmployee.GetEmployeeList(new Hashtable
            {
                {"ID", userId}
            });

            if (dtEmployee.Rows.Count == 0)
            {
                return "当前用户不存在！";
            }

            var employee = dtEmployee.AsEnumerable().Select(dr => new EmployeeModel
            {
                //Authority = Convert.ToInt32(dr["Authority"]) & (~Convert.ToInt32(authorityId)),//使用与运算删除权限
                AuthorityCode = CommonUtil.RemoveAuthorityCode(dr["AuthorityCode"].ToString(), authorityCode),
                Code = dr["Code"].ToString(),
                Description = dr["Description"].ToString(),
                Id = Convert.ToInt32(dr["Id"]),
                LoginName = dr["LoginName"].ToString(),
                Name = dr["Name"].ToString(),
                Password = dr["Password"].ToString()
            }).ToList().First();

            return _dalEmployee.ModifyEmployee(employee.ToHashTable());
        }


        internal EmployeeModel GetEmployeeWithAuthority(string userId)
        {
            var employee = new EmployeeModel {Id = Convert.ToInt32(userId)};
            //查询用户基本信息
            var empList = GetEmployeeList(employee).ToList();
            if (!empList.Any())
                return employee;
            //设置基本信息
            employee = empList.First();
            //查询所有角色以及权限
            var roleList = _dalRole.GetRoleList().AsEnumerable().Select(dr => new RoleModel
            {
                //Authority = Convert.ToInt32(dr["Authority"]),
                AuthorityCode =dr["AuthorityCode"].ToString(),
                Id = Convert.ToInt32(dr["Id"]),
                Description = dr["Description"].ToString(),
                Name = dr["Name"].ToString()
            }).ToList();
            var authList = _dalAuthority.GetAuthorityList().AsEnumerable().Select(dr=>new AuthorityModel
            {
                AuthorityCode = dr["AuthorityCode"].ToString(),
                //AuthorityId = Convert.ToInt32(dr["AuthorityId"]),
                Description = dr["Description"].ToString(),
                Name = dr["Name"].ToString(),
                Id=Convert.ToInt32(dr["Id"])
            }).ToList();

            employee.RoleList = roleList;
            employee.AuthorityList = authList;
            //查询该用户的相关角色
            var empRoleList = _dalEmpRoles.GetEmpRoleList(new Hashtable
            {
                {"emp_id",userId}
            }).AsEnumerable().Select(dr => new EmpRoleModel
            {
                EmpId = Convert.ToInt32(dr["emp_id"]),
                RoleId = Convert.ToInt32(dr["role_id"])
            }).ToList();
            employee.EmpRoleList = empRoleList;

            return employee;
        }


        public string GetUserAuthorityCode(string userId)
        {
            var employee = GetEmployeeWithAuthority(userId);
            var authorityCode =
                employee.EmpRoleList.Select(
                    er => employee.RoleList.Where(r => r.Id == er.RoleId).Select(r => r.AuthorityCode).FirstOrDefault())
                    .Aggregate(employee.AuthorityCode,
                        CommonUtil.AppendAuthorityCode);
            return authorityCode;

        }




    }
}
