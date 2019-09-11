using OnlineLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace OnlineLibrary.Data.DTOs
{
    public class CreateOrEditBookDTO
    {
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<Writer> Writers { get; set; }

        public List<Genre> Genres { get; set; }

        public int SelectedWriterID { get; set; }

        public int SelectedGenreID { get; set; }

        public string Description { get; set; }
    }
}
