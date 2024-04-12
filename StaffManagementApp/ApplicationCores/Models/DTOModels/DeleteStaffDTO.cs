using System.ComponentModel.DataAnnotations;

namespace StaffManagementApp.ApplicationCores.Models.DTOModels
{
    public class DeleteStaffDTO
    {
        [StringLength(8)]
        [Required]
        public string StaffId { get; set; }
        
    }
}
