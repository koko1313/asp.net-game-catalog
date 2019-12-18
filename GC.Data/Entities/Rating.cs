using System.ComponentModel.DataAnnotations;

namespace GC.Data.Entities
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Display(Name = "Rating")]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string RatingValue { get; set; }

        [Required]
        [StringLength(400, MinimumLength = 1)]
        public string Description { get; set; }

        //public virtual ICollection<Movie> Movies { get; set; }
    }
}