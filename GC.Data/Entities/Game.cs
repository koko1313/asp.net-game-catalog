using System;
using System.ComponentModel.DataAnnotations;

namespace GC.Data.Entities
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; }

        //[Required]
        public string ReleaseYear { get; set; }

        [Display(Name = "Genre")]
        public int? GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        [Display(Name = "Rating")]
        public int? RatingId { get; set; }
        public virtual Rating Rating { get; set; }
    }
}