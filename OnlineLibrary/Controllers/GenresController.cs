using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Data.Models;
using OnlineLibrary.Data.Repos;
using OnlineLibrary.Helpers;
using System.Linq;

namespace OnlineLibrary.Controllers
{

    [Authorize(Roles = "Admin,User")]
    public class GenresController : Controller
    {
        public IGenericRepository<Genre> GenresRepository { get; set; }

        public GenresController(IGenericRepository<Genre> genresRepository)
        {
            this.GenresRepository = genresRepository;
        }


        [AllowAnonymous]
        public IActionResult Index(int? page)
        {
            var writers = GenresRepository.GetAll().ToList();

            var pagenatedList = PaginatedList<Genre>.Create(writers, page ?? 1, GlobalConstants.pageSize);
            return View(pagenatedList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("GenreName")] Genre genre)
        {
            if (string.IsNullOrEmpty(genre.GenreName))
            {
                return View(genre);
            }
            else
            {
                this.GenresRepository.Insert(genre);
                this.GenresRepository.Save();
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit([Bind("GenreName")] Genre genre, int id)
        {
            var persistedGenre = this.GenresRepository.GetById(id);

            if (string.IsNullOrEmpty(genre.GenreName))
            {
                return View(persistedGenre);
            }
            else
            {
                persistedGenre.GenreName = genre.GenreName;
                this.GenresRepository.Update(persistedGenre);
                this.GenresRepository.Save();
                return RedirectToAction("Index");
            }
        }
    }
}