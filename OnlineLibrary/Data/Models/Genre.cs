using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Data.Models
{
    public class Genre : AbstractEntity
    {

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string GenreName { get; set; }

        public List<Book> Books { get; set; }
    }
}
