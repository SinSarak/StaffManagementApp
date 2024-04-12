using System.ComponentModel.DataAnnotations;

namespace StaffManagementApp.Domain.Entities
{
    public class Staff
    {

        [Key]
        public int Id { get; set; }
        [StringLength(8)]
        [Required]
        public string StaffId { get; set; }
        [StringLength(100)]
        [Required]
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        [Range(1, 2)]
        //1=Male, 2=Female
        public int Gender { get; set; }
    }
}
