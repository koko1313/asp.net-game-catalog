using System;
using System.ComponentModel.DataAnnotations;

namespace GC.Data.Entities
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string GenreName { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }
    }
}