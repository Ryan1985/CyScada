using System;
using System.Collections;
using System.ComponentModel;

namespace CyScada.Common
{
    public static class CommonExtensions
    {
        /// <summary>
        /// 将类实例映射到哈希表
        /// </summary>
        /// <typeparam name="T">需要映射的类型</typeparam>
        /// <param name="obj">需要映射的类型实例</param>
        /// <returns></returns>
        public static Hashtable ToHashTable<T>(this T obj)
        {
            var htResult = new Hashtable();
            var properties = typeof (T).GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(obj, null);
                if (value == null)
                    continue;
                htResult.Add(property.Name, property.GetValue(obj, null));
            }
            return htResult;
        }


        public static T? ConvertTo<T>(this IConvertible convertibleValue) where T : struct
        {
            if (null == convertibleValue)
            {
                return null;
            }
            return (T?) Convert.ChangeType(convertibleValue, typeof (T));
        }


        public static T? ConvertToNullable<T>(this object obj) where T : struct
        {
            var converter = new NullableConverter(typeof(T?));
            return (T?)converter.ConvertFromString(obj.ToString());
        }



    }
}
