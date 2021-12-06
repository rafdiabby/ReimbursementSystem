using API.Models;
using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class CategoryController : BaseController<Category, CategoryRepository, int>
    {
        private readonly CategoryRepository repository;
        public CategoryController(CategoryRepository repository) : base(repository)
        {
            this.repository = repository;
        }
            
    

        public IActionResult Index()
        {
            return View();
        }

    }
}
