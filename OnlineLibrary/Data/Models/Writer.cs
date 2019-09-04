﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Data.Models
{
    public class Writer
    {
        [Key]
        public int WriterId { get; set; }

        [StringLength(200, MinimumLength = 1)]
        public string FirstName { get; set; }

        [StringLength(200, MinimumLength = 1)]
        public string LastName { get; set; }

        public string UserName { get; set; }

    }
}
