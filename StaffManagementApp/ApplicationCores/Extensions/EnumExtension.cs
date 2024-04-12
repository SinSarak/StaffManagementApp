using System.ComponentModel;
using System.Reflection;

namespace StaffManagementApp.ApplicationCores.Extensions
{
    public static class Enum<T> where T : struct, IConvertible
    {
        public static List<KeyValuePair<int, string>> GetEnumList()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enum.");
            }

            var list = new List<KeyValuePair<int, string>>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                FieldInfo fi = e.GetType().GetField(e.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                string Key = "";


                if (attributes.Length > 0)
                {
                    Key = attributes[0].Description;
                }
                else
                {
                    Key = e.ToString();
                }

                list.Add(new KeyValuePair<int, string>((int)e, Key));
            }
            return list;
        }
    }
}
