using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Data.Models
{
    public class Writer : AbstractEntity
    {

        [StringLength(200, MinimumLength = 1)]
        public string FirstName { get; set; }

        [StringLength(200, MinimumLength = 1)]
        public string LastName { get; set; }

        public string UserName { get; set; }

        public List<Book> Books { get; set; }

    }
}
