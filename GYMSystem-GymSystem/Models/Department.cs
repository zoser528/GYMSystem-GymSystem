using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYMSystem.Models
{
    [Table("Department", Schema = "dbo")]

    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Department Code")]
        public string DepartmentCode { get; set; }
        [Required]
        [Display(Name = "still work")]
        public bool stillWork { get; set; }

    }
}
