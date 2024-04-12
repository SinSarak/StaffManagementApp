using System.ComponentModel;

namespace StaffManagementApp.ApplicationCores.ValueObjects
{
    public static class AppValueObjects
    {
        public enum StaffGender
        {
            [Description("Male")]
            Male = 1,
            [Description("Female")]
            Female = 2,
        }
    }
}
