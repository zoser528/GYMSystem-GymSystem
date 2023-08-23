using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYMSystem.Models
{

    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        [Column(TypeName = "varchar(150)")]
        [MaxLength(100)]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "Employee Number")]
        [Column(TypeName = "varchar(5)")]
        [MaxLength(5)]
        public string EmployeeNumber { get; set; }

        [Required]
        [Display(Name = "date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yy}")]
        public DateTime DOB { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Required]
        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }


        [Required]
        [Column(TypeName = "decimal(12,2)")]
        [Display(Name = "Gross Salary")]
        public decimal GrossSalary { get; set; }


        [Required]
        [Column(TypeName = "decimal(12,2)")]
        [Display(Name = "Net Salary")]
        public decimal NetSalary { get; set; }

    }
}
