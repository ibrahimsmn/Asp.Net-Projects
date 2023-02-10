using GameNews.Models;
using GameNews.RepositoryDesingPattern.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameNews.Areas.Management.Controllers
{
    [Area("management")]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        readonly IRepositoryBase<Category> _repo;
        public CategoryController(IRepositoryBase<Category> repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category model)
        {
            _repo.Add(model);
            return RedirectToAction("Index");
        }
    }
}
