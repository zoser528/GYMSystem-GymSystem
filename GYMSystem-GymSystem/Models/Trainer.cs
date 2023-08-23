using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYMSystem.Models
{
    [Table("Trainer", Schema = "dbo")]
    public class Trainer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainerId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "trainer Name")]
        public string TrainerName { get; set; }
        [StringLength(50)]
        [Display(Name = "still work")]
        public string stillWork { get; set; }
        [Required]  
        [Display(Name = "trainer salary")]
        public int trainerSalary { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        [Required]
        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }

        [Required]
        [Display(Name = "Trainer Number")]
        [MaxLength(11)]
        public string trainerNumber { get; set; }
        [Required]
        [Display(Name = "trainer address")]
        public string trainerAddress { get; set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "trainer email")]
        public string trainerEmail { get; set; }

        [Required]
        [Display(Name = "Subscription Salary")]
        public int SubscriptionSalary { get; set; }

        public int Counter { get; set; }


    }
}
