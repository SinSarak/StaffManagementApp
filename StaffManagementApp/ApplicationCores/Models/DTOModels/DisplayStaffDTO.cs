using System.ComponentModel.DataAnnotations;

namespace StaffManagementApp.ApplicationCores.Models.DTOModels
{
    public class DisplayStaffDTO
    {
        [StringLength(8)]
        public string StaffId { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }

        //1=Male, 2=Female
        public int Gender { get; set; }
        public string GenderText { get; set; }
    }
}
