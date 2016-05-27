using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyScada.Common
{
    public class CommonUtil
    {
        public const string DefaultUserPassword="118DD9CC92B899DF";//123456加密
        public const string AuthSeperator = ",";

        public enum MappingType
        {
            UserTheme
        }

        public static string Encrypt(string password)
        {
            return DESEncrypt.Encrypt(password);
        }

        public static string Decrypt(string password)
        {
            return DESEncrypt.Decrypt(password);
        }


        public static string AppendAuthorityCode(string currentAuth,string authCode)
        {
            if (string.IsNullOrEmpty(authCode))
            {
                return currentAuth;
            }
            if (!authCode.Contains(AuthSeperator))
            {
                authCode = authCode + AuthSeperator;
            }

            if (!currentAuth.Contains(authCode))
            {
                currentAuth += authCode;
            }
            return currentAuth;
        }

        public static string RemoveAuthorityCode(string currentAuth, string authCode)
        {
            if (string.IsNullOrEmpty(authCode))
            {
                return currentAuth;
            }
            if (!authCode.Contains(AuthSeperator))
            {
                authCode = authCode + AuthSeperator;
            }

            if (currentAuth.Contains(authCode))
            {
                currentAuth = currentAuth.Replace(authCode, string.Empty);
            }
            return currentAuth;
        }

        public static bool ExistAuthorityCode(string currentAuth, string authCode)
        {
            if (string.IsNullOrEmpty(authCode))
            {
                return false;
            }
            if (!authCode.Contains(AuthSeperator))
            {
                authCode = authCode + AuthSeperator;
            }

            return currentAuth.Contains(authCode);
        }
    }
}
