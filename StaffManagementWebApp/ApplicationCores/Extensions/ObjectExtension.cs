using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace StaffManagementWebApp.ApplicationCores.Extensions
{
    public static class ObjectExtension
    {
        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static bool IsNullObject(this object src)
        {
            return (src == null) ? true : false;
        }

        public static Dictionary<string, string> ToDictionary(this object src)
        {
            Dictionary<string, string> paramsString = new Dictionary<string, string>();
            if (src == null) throw new Exception("Object is null or empty.");

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(src))
            {
                object value = property.GetValue(src);

                if (value == typeof(DateTime))
                {
                    paramsString.Add(property.Name, $"{((DateTime)value).ToString("s")}Z");
                }
                else
                {
                    paramsString.Add(property.Name, value.ToString());
                }
                
                
            }
            return paramsString;
        }
    }
}
