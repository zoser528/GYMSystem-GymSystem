using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GYMSystem.Models
{
    [Table("Branch", Schema = "dbo")]

    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Branch Id")]
        public int BranchId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Branch Location")]
        public string BranchLocation { get; set; }

        [Required]
        [Display(Name = "Branch Status")]
        public bool BranchStatus { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Work Start")]
        public string WorkStart { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Work End")]
        public string WorkEnd { get; set; }

    }
}
