using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyScada.Model
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string ReadName { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public string UserType { get; set; }
        public string ErrorMessage { get; set; }
        public string TokenKey { get; set; }
    }
}
