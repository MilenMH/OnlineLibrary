using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Data.Models;
using OnlineLibrary.Data.Repos;
using OnlineLibrary.Helpers;
using System.Linq;

namespace OnlineLibrary.Controllers
{

    [Authorize(Roles = "Admin,User")]
    public class WritersController : Controller
    {
        public IGenericRepository<Writer> WritersRepository { get; set; }


        public WritersController(IGenericRepository<Writer> writersRepository)
        {
            this.WritersRepository = writersRepository;
        }


        [AllowAnonymous]
        public IActionResult Index(int? page)
        {
            var writers = this.WritersRepository.GetAll().ToList();

            var pagenatedList = PaginatedList<Writer>.Create(writers, page ?? 1, GlobalConstants.pageSize);
            return View(pagenatedList);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Edit([Bind("FirstName, LastName, UserName")] Writer writer, int id)
        {
            var persistedWriter = this.WritersRepository.GetById(id);

            if (string.IsNullOrEmpty(writer.UserName))
            {
                return View(persistedWriter);
            }
            else
            {
                persistedWriter.FirstName = writer.FirstName;
                persistedWriter.LastName = writer.LastName;
                persistedWriter.UserName = writer.UserName;
                this.WritersRepository.Update(persistedWriter);
                this.WritersRepository.Save();
                return RedirectToAction("Index");
            }
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("FirstName, LastName, UserName")] Writer writer)
        {
            if (string.IsNullOrEmpty(writer.UserName))
            {
                return View(writer);
            }
            else
            {
                this.WritersRepository.Insert(writer);
                this.WritersRepository.Save();
                return RedirectToAction("Index");
            }
        }
    }
}