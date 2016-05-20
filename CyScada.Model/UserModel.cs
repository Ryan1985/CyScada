using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyScada.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string ErrorMessage { get; set; }
        //public int Authority { get; set; }
        public string AuthorityCode { get; set; }
        public string Code { get; set; }
        public string TokenKey { get; set; }
    }
}
