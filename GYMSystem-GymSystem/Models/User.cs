using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYMSystem.Models
{
    [Table("User", Schema = "dbo")]

    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "User Email")]
        public string UserEmail { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "User Phone")]
        public string UserPhone { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "User Address")]
        public string UserAddress { get; set; }

    }
}
