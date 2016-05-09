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

        public static string CreateString<T>(T obj)
        {
            string ret = "";
            foreach(PropertyInfo prop in obj.GetType().GetProperties())
            {

                var minifyAs = prop.GetCustomAttribute<Niklas.MicroifyForQR.MinifyAs>();
                if(minifyAs != null)
                {
                    ret += minifyAs.MinifiedProperty;
                }
                else
                {
                    ret += prop.Name.Substring(0,3);
                }

                ret += ":";

                ret += prop.GetValue(obj);

                ret += "-";
            }
            return ret;
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
