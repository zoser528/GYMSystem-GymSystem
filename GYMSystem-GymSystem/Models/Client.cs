using GYMSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYMSystem_GymSystem.Models
{

    [Table("Client", Schema = "dbo")]

    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Client Id")]
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "Client Name")]
        [Column(TypeName = "varchar(150)")]
        [MaxLength(100)]
        public string ClientName { get; set; }

        

        [Required]
        [Display(Name = "Client Number")]
        [Column(TypeName = "varchar(11)")]
        [MaxLength(11)]
        public string ClientNumber { get; set; }

        [Required]
        [Display(Name = "date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yy}")]
        public DateTime DOB { get; set; }


        [Required]
        [Display(Name = "Client address")]
        public string ClientAddress { get; set; }

        [Display(Name = "Date of end Subscription")]
        public DateTime End_Subscription { get; set; }

        [Display(Name = "paid or Not")]
        public bool pay { get; set; }

        [Display(Name = "Actived or Not")]
        public bool Active { get; set; }


        [Required]
        [ForeignKey("Branch")]
        public int Branchid { get; set; }

        [Display(Name = "Branch")]
        [NotMapped]
        public string? BranchName { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int Departmentid { get; set; }

        [Display(Name = "Department")]
        [NotMapped]
        public string? DepartmentName { get; set; }

        [Required]
        [ForeignKey("Trainer")]
        public int? Trainerid { get; set; }

        [Display(Name = "Trainer")]
        [NotMapped]
        public string? TrainerName { get; set; }

        [Required]
        [ForeignKey("Subscription")]
        public int Subscriptionid { get; set; }

        [Display(Name = "Subscription")]
        [NotMapped]
        public string? SubscriptionName { get; set; }



        public virtual Department Department { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual Trainer? Trainer { get; set; }

    }
}
