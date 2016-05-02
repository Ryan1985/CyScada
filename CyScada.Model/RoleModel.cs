using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyScada.Model
{
    public class RoleModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Authority { get; set; }
        /// <summary>
        /// 所有的可用权限列表
        /// </summary>
        public IList<AuthorityModel> AuthorityList { get; set; }
    }
}
