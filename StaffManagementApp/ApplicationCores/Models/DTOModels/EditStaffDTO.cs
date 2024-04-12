using System.ComponentModel.DataAnnotations;

namespace StaffManagementApp.ApplicationCores.Models.DTOModels
{
    public class EditStaffDTO
    {
        [StringLength(8)]
        [Required]
        public string EditId { get; set; }
        [StringLength(100)]
        [Required]
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }

        //1=Male, 2=Female
        public int Gender { get; set; }
    }
}
