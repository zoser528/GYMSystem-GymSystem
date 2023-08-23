using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYMSystem.Models
{
    [Table("Subscription", Schema = "dbo")]

    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubscriptionId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Subscription Name")]
        public string SubscriptionName { get; set; }
        [Required]
        [Display(Name = "Subscription Price")]
        public int SubscriptionPrice { get; set; }
        [Required]
        [Display(Name = "Subscription Period")]
        public int SubscriptionPeriod { get; set; }
        [Required]
        [Display(Name = "Subscription Status")]
        public bool SubscriptionStatus { get; set; }

    }
}
