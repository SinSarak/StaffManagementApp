using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.RegularExpressions;

namespace StaffManagementApp.ApplicationCores.Extensions
{
    public static class StringExtension
    {
        public static string ToCapitalFirst(this string inStr)
        {
            if (!string.IsNullOrWhiteSpace(inStr))
            {
                string clone = inStr.Trim();

                return clone[0].ToString().ToUpper() + clone.Remove(0, 1);
            }
            return inStr;
        }

        public static string ToCapitalNullable(this string inStr)
        {
            if (inStr != null)
            {
                return inStr.ToUpper();
            }
            return "";
        }

        public static string ToCapitalFirstCharEachWord(this string inStr)
        {
            if (!string.IsNullOrEmpty(inStr))
            {
                string result = "";
                foreach (var s in inStr.TrimNullable().Split(' '))
                {
                    result += s[0];
                }
                return result.ToCapitalNullable();
            }
            return "";
        }
        public static string ToAPICode(this string inStr)
        {
            // first trim the raw string
            string safe = inStr.Trim();

            // replace spaces with hyphens
            safe = safe.Replace(" ", "_");

            // replace any 'double spaces' with singles
            if (safe.IndexOf("--") > -1)
                while (safe.IndexOf("--") > -1)
                    safe = safe.Replace("--", "_");

            // trim out illegal characters
            safe = Regex.Replace(safe, "[^a-zA-Z0-9-_]", "");

            // trim the length
            if (safe.Length > 100)
                safe = safe.Substring(0, 99);

            return safe;
        }

        public static string SanitizedAsFileName(this string inStr)
        {
            // first trim the raw string
            string safe = inStr.Trim();

            // replace spaces with hyphens
            safe = safe.Replace(" ", "-");

            // replace any 'double spaces' with singles
            if (safe.IndexOf("--") > -1)
                while (safe.IndexOf("--") > -1)
                    safe = safe.Replace("--", "-");

            // trim out illegal characters
            safe = Regex.Replace(safe, "[^a-zA-Z0-9\\-]", "");

            // trim the length
            if (safe.Length > 50)
                safe = safe.Substring(0, 49);

            // clean the beginning and end of the filename
            char[] replace = { '-', '.' };
            safe = safe.TrimStart(replace);
            safe = safe.TrimEnd(replace);

            return safe;
        }
        public static string SanitizedAsFileNameUnicode(this string inStr)
        {
            // first trim the raw string
            string safe = inStr.Trim();

            // replace spaces with hyphens
            safe = safe.Replace(" ", "-");

            // replace any 'double spaces' with singles
            if (safe.IndexOf("--") > -1)
                while (safe.IndexOf("--") > -1)
                    safe = safe.Replace("--", "-");

            // trim out illegal characters
            //safe = Regex.Replace(safe, "[^a-zA-Z0-9\\-]", "");

            // trim the length
            if (safe.Length > 50)
                safe = safe.Substring(0, 49);

            // clean the beginning and end of the filename
            char[] replace = { '-', '.' };
            safe = safe.TrimStart(replace);
            safe = safe.TrimEnd(replace);

            return safe;
        }

        public static string TrimNullable(this string inStr)
        {
            if (!string.IsNullOrEmpty(inStr))
            {
                return inStr.Trim();
            }
            return "";
        }
        public static string SubStringNullable(this string inStr, int LimitedLength)
        {
            if (!string.IsNullOrEmpty(inStr))
            {
                if (inStr.Trim().Length > LimitedLength && LimitedLength != 0)
                {
                    return inStr.Trim().Substring(0, LimitedLength);
                }
                return inStr.Trim();
            }
            return "";
        }

        public static string ToUpperNullable(this string inStr)
        {
            if (!string.IsNullOrEmpty(inStr))
            {
                return inStr.ToUpper();
            }
            return "";
        }

        public static string ToLowerNullable(this string inStr)
        {
            if (!string.IsNullOrEmpty(inStr))
            {
                return inStr.ToLower();
            }
            return "";
        }

        public static string[] SplitNullable(this string inStr, string separator)
        {
            if (inStr != null)
            {
                return inStr.Split(separator);
            }
            return new string[0];
        }
        public static List<string> SplitNullableToList(this string inStr, string separator)
        {
            if (inStr != null)
            {
                return inStr.Split(separator).ToList();
            }
            return new List<string>();
        }

        public static string PreventNull(this string inStr)
        {
            if (inStr == null)
            {
                return "";
            }
            return inStr;
        }

        public static DateTime[] ToDatetimeRange(this string rangedatetime, string originalFormat) //"MM/dd/yyyy"
        {
            DateTime[] result = new DateTime[2];
            var datetimes = rangedatetime.Split('-');


            try
            {
                if (datetimes.Length != 2)
                {
                    throw new Exception("Datetime range is invalid format.");
                }

                if (!string.IsNullOrEmpty(datetimes[0]))
                {
                    result[0] = DateTime.ParseExact(datetimes[0].Trim(), originalFormat, null,
                               System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                               System.Globalization.DateTimeStyles.AdjustToUniversal);
                }
                if (!string.IsNullOrEmpty(datetimes[1]))
                {
                    result[1] = DateTime.ParseExact(datetimes[1].Trim(), originalFormat, null,
                               System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                               System.Globalization.DateTimeStyles.AdjustToUniversal);
                }
            }
            catch
            {
                throw new Exception("Failed to convert string to datetime range");
            }
            return result;
        }

        public static DateTime[] ToMinMaxDatetimeRange(this string rangedatetime, string originalFormat) //"MM/dd/yyyy"
        {
            DateTime[] result = new DateTime[2];
            var datetimes = rangedatetime.Split('-');


            try
            {
                if (datetimes.Length != 2)
                {
                    throw new Exception("Datetime range is invalid format.");
                }

                if (!string.IsNullOrEmpty(datetimes[0]))
                {
                    result[0] = DateTime.ParseExact(datetimes[0].Trim(), originalFormat, null,
                               System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                               System.Globalization.DateTimeStyles.AdjustToUniversal).MinDateTime();
                }
                if (!string.IsNullOrEmpty(datetimes[1]))
                {
                    result[1] = DateTime.ParseExact(datetimes[1].Trim(), originalFormat, null,
                               System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                               System.Globalization.DateTimeStyles.AdjustToUniversal).MaxDateTime();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to convert string to datetime range");
            }
            return result;
        }

        public static bool TryToDateTime(this string datetime, string originalFormat, out DateTime DateTimeValue) //"MM/dd/yyyy"
        {
            DateTimeValue = new DateTime();

            try
            {

                if (!string.IsNullOrEmpty(datetime))
                {
                    DateTimeValue = DateTime.ParseExact(datetime.Trim(), originalFormat, null,
                               System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                               System.Globalization.DateTimeStyles.AdjustToUniversal);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static DateTime ToDateTime(this string datetime, string originalFormat) //"MM/dd/yyyy"
        {
            DateTime result = new DateTime();

            try
            {

                if (!string.IsNullOrEmpty(datetime))
                {
                    result = DateTime.ParseExact(datetime.Trim(), originalFormat, null,
                               System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                               System.Globalization.DateTimeStyles.AdjustToUniversal);
                }

            }
            catch (Exception)
            {
                throw new Exception("Failed to convert string to datetime");
            }
            return result;
        }

        public static DateTime ToDateTimeOrDefaultDateTime(this string datetime, string originalFormat) //"MM/dd/yyyy"
        {
            DateTime result = new DateTime();

            try
            {

                if (!string.IsNullOrEmpty(datetime))
                {
                    result = DateTime.ParseExact(datetime.Trim(), originalFormat, null,
                               System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                               System.Globalization.DateTimeStyles.AdjustToUniversal);
                }

            }
            catch (Exception)
            {
                return DateTime.Now.GetNullDate();
            }
            return DateTime.Now.GetNullDate();
        }

        public static DateTime? ToDateTimeNullable(this string datetime, string originalFormat) //"MM/dd/yyyy"
        {
            DateTime result = new DateTime();

            try
            {

                if (!string.IsNullOrEmpty(datetime))
                {
                    result = DateTime.ParseExact(datetime.Trim(), originalFormat, null,
                               System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                               System.Globalization.DateTimeStyles.AdjustToUniversal);
                }

            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public static bool IsValidJsonNullable(this string jsonString)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonString) || jsonString == "[]" || jsonString == "{}")
                {
                    return true;
                }

                JToken.Parse(jsonString);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }

        }

        public static DateTime ToExactJsonDateTime(this string datetime)
        {
            DateTime result = new DateTime();

            try
            {

                if (!string.IsNullOrEmpty(datetime))
                {
                    result = DateTime.ParseExact(datetime, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                }

            }
            catch (Exception)
            {
                throw new Exception("Failed to convert string to datetime");
            }
            return result;
        }
    }
}
