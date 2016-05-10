using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Niklas.MicroifyForQR
{
    public class MinifyForQR
    {

        private const char KV_SEP = ':';
        private const char PROP_SEP = '-';

        public static string CreateString<T>(T obj)
        {
            string ret = "";
            foreach(PropertyInfo prop in obj.GetType().GetProperties())
            {
                if (IsIgnored(prop))
                    continue;
                var minifyAs = prop.GetCustomAttribute<Niklas.MicroifyForQR.MinifyAs>();
                if (minifyAs != null)
                {
                    ret += minifyAs.MinifiedProperty;
                }
                else
                {
                    ret += AutoMinify(prop.Name);
                }

                ret += KV_SEP;

                ret += prop.GetValue(obj);

                ret += PROP_SEP;
            }
            return ret;
        }

        public static T Parse<T>(T obj, string str)
        {
            Dictionary<string, string> kvs = InitializePropertyValuePairs(str.Split(PROP_SEP));

            foreach (var prop in obj.GetType().GetProperties())
            {
                string key = "";
                var minifyAs = prop.GetCustomAttribute<Niklas.MicroifyForQR.MinifyAs>();
                var ignore = prop.GetCustomAttribute<Niklas.MicroifyForQR.IgnoreProperty>();
                if (minifyAs != null)
                    key = minifyAs.MinifiedProperty;
                else if (ignore != null)
                    continue;
                else
                    key = AutoMinify(prop.Name);

                prop.SetValue(obj, kvs[key]);
            }

            return obj;
        }

        private static Dictionary<string, string> InitializePropertyValuePairs(string[] props)
        {
            Dictionary<string, string> kvs = new Dictionary<string, string>();
            foreach (string pr in props)
            {
                if (pr != "")
                {
                    string[] arr = pr.Split(KV_SEP);
                    kvs.Add(arr[0], arr[1]);
                }
            }

            return kvs;
        }

        private static string AutoMinify(string str)
        {
            return (str.Length > 2)?str.Substring(0,3):str;
        }

        private static bool IsIgnored(PropertyInfo prop)
        {
            return (prop.GetCustomAttribute<Niklas.MicroifyForQR.IgnoreProperty>() != null);
        }
    }


    [System.AttributeUsage(AttributeTargets.Property)]
    public class MinifyAs:System.Attribute
    {
        public string MinifiedProperty { get; set; }
        public MinifyAs(string minifiedPropertyName = "")
        {
            MinifiedProperty = minifiedPropertyName;
        }
    }

    [System.AttributeUsage(AttributeTargets.Property)]
    public class IgnoreProperty:System.Attribute
    {

    }
}
