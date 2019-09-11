using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Data.DTOs;
using OnlineLibrary.Data.Models;
using OnlineLibrary.Data.Repos;
using OnlineLibrary.Helpers;

namespace OnlineLibrary.Controllers
{

    [Authorize(Roles = "Admin,User")]
    public class BooksController : Controller
    {

        public IBookRepository BooksRepository { get; set; }
        public IGenericRepository<Genre> GenresRepository { get; set; }
        public IGenericRepository<Writer> WritersRepository { get; set; }


        public BooksController(IBookRepository booksRepository, IGenericRepository<Genre> genresRepository, IGenericRepository<Writer> writersRepository)
        {
            this.BooksRepository = booksRepository;
            this.WritersRepository = writersRepository;
            this.GenresRepository = genresRepository;
        }


        [AllowAnonymous]
        public IActionResult Index(string searchByBookTitle, string searchByGenreName, string searchByWriterName, int? page)
        {
            var books = BooksRepository.GetAll().ToList();

            if (!string.IsNullOrEmpty(searchByBookTitle))
            {
                books = books.Where(b => b.Title.Contains(searchByBookTitle)).ToList();
            }

            if (!string.IsNullOrEmpty(searchByGenreName))
            {
                books = books.Where(b => b.Genre.GenreName.Contains(searchByGenreName)).ToList();
            }

            if (!string.IsNullOrEmpty(searchByWriterName))
            {
                books = books.Where(b =>
                    b.Writer.FirstName.Contains(searchByWriterName) || b.Writer.LastName.Contains(searchByWriterName)).ToList();
            }

            var pagenatedList = PaginatedList<Book>.Create(books, page ?? 1, GlobalConstants.pageSize);
            pagenatedList.SearchByBookTitle = searchByBookTitle;
            pagenatedList.SearchByGenreName = searchByGenreName;
            pagenatedList.SearchByWriterName = searchByWriterName;
            if (pagenatedList.Genres == null)
            {
                pagenatedList.Genres = new List<Genre>() { new Genre() { GenreName = string.Empty } };
                pagenatedList.Genres.AddRange(GenresRepository.GetAll().ToList());

                if (!string.IsNullOrEmpty(searchByGenreName))
                {
                    var index = pagenatedList.Genres.FindIndex(g => g.GenreName.Contains(searchByGenreName));
                    var temp = pagenatedList.Genres[0];
                    pagenatedList.Genres[0] = pagenatedList.Genres[index];
                    pagenatedList.Genres[index] = temp;
                }
            }
            return View(pagenatedList);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Title, ReleaseDate, Description, SelectedWriterID, SelectedGenreID")] CreateOrEditBookDTO createBookDTO)
        {
            if (string.IsNullOrEmpty(createBookDTO.Title))
            {
                createBookDTO.Writers = this.WritersRepository.GetAll().ToList();
                createBookDTO.Genres = this.GenresRepository.GetAll().ToList();

                return View(createBookDTO);
            }
            else
            {
                var res = this.BooksRepository.Insert(new Book()
                {
                    Title = createBookDTO.Title,
                    ReleaseDate = createBookDTO.ReleaseDate,
                    IdWriter = createBookDTO.SelectedWriterID,
                    IdGenre = createBookDTO.SelectedGenreID,
                    Description = createBookDTO.Description
                });
                this.BooksRepository.Save();
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit([Bind("Title, ReleaseDate, Description, SelectedWriterID, SelectedGenreID")] CreateOrEditBookDTO createBookDTO, int id)
        {
            var book = this.BooksRepository.GetById(id);

            if (string.IsNullOrEmpty(createBookDTO.Title))
            {
                createBookDTO.Writers = this.WritersRepository.GetAll().ToList();
                createBookDTO.Genres = this.GenresRepository.GetAll().ToList();

                var indexOfWriter = createBookDTO.Writers.FindIndex(b =>  b.Id == book.IdWriter);
                var tempWriter = createBookDTO.Writers[0];
                createBookDTO.Writers[0] = createBookDTO.Writers[indexOfWriter];
                createBookDTO.Writers[indexOfWriter] = tempWriter;

                var indexOfGenre = createBookDTO.Genres.FindIndex(g => g.Id == book.IdGenre);
                var tempGenre = createBookDTO.Genres[0];
                createBookDTO.Genres[0] = createBookDTO.Genres[indexOfGenre];
                createBookDTO.Genres[indexOfGenre] = tempGenre;

                createBookDTO.Title = book.Title;
                createBookDTO.ReleaseDate = book.ReleaseDate;
                createBookDTO.Description = book.Description;

                return View(createBookDTO);
            }
            else
            {
                book.Title = createBookDTO.Title;
                book.ReleaseDate = createBookDTO.ReleaseDate;
                book.Description = createBookDTO.Description;
                book.IdGenre = createBookDTO.SelectedGenreID;
                book.IdWriter = createBookDTO.SelectedWriterID;
                this.BooksRepository.Update(book);
                this.BooksRepository.Save();

                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            this.BooksRepository.Delete(id);
            this.BooksRepository.Save();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Details(int id)
        {
            var book = BooksRepository.GetById(id);

            return View(book);
        }

    }
}