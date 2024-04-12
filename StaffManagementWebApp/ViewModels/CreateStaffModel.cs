using System.ComponentModel.DataAnnotations;

namespace StaffManagementWebApp.ViewModels
{
    public class CreateStaffModel
    {
        [StringLength(8)]
        [Required]
        public string StaffId { get; set; }
        [StringLength(100)]
        [Required]
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }

        //1=Male, 2=Female
        public int Gender { get; set; }
    }
}
