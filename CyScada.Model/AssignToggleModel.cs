using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyScada.Model
{
    public class AssignToggleModel
    {
        public string Id { get; set; }
        /// <summary>
        /// 操作类型:(Add:添加，Delete:删除)
        /// </summary>
        public string ToggleType { get; set; }
        public string UserId { get; set; }
        /// <summary>
        /// 操作对象:(UserAuthority:用户权限,UserRole:用户角色,RoleAuthority:角色权限)
        /// </summary>
        public string ToggleHost { get; set; }
        

    }
}
