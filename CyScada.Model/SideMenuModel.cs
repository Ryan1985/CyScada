using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyScada.Model
{
    public class SideMenuModel
    {
        public int? Id { get; set; }
        //public int? AuthorityId { get; set; }
        public string AuthorityCode { get; set; }
        public string AuthorityName { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string ClassName { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        public int? SortNumber { get; set; }
        public string SideMenuDesc { get; set; }
        public List<SideMenuModel> SubMenus { get; set; }

}
}
