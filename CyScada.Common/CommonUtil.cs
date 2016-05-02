using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyScada.Common
{
    public class CommonUtil
    {
        public const string DefaultUserPassword="118DD9CC92B899DF";//123456加密

        public static string Encrypt(string password)
        {
            return DESEncrypt.Encrypt(password);
        }

        public static string Decrypt(string password)
        {
            return DESEncrypt.Decrypt(password);
        }

    }
}
