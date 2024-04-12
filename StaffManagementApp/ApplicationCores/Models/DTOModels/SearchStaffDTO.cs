using System.ComponentModel.DataAnnotations;

namespace StaffManagementApp.ApplicationCores.Models.DTOModels
{
    public class SearchStaffDTO
    {
        public string? StaffId { get; set; }
        //yyyy-MM-dd
        public DateTime? FromDate { get; set; }
        //yyyy-MM-dd
        public DateTime? ToDate { get; set; }
        //1=Male, 2=Female
        public int Gender { get; set; }
    }
}
