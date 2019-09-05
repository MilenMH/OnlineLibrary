using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Data.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required]
        public Writer Writer { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [StringLength(500, MinimumLength = 1)]
        public string Description { get; set; }
    }
}
