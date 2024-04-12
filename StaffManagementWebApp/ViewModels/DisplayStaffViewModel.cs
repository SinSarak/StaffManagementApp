using System.ComponentModel.DataAnnotations;

namespace StaffManagementWebApp.ViewModels
{
    public class DisplayStaffViewModel
    {
        public string StaffId { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        //1=Male, 2=Female
        public int Gender { get; set; }
        public string GenderText { get; set; }
    }
}
