using Client.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ReimburseController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        public ReimburseController(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View(new CreatePost());
        }

        [HttpPost]
        public IActionResult Create(CreatePost model)
        {
            // do other validations on your model as needed
            if (model.MyImage != null)
            {
                foreach (var item in model.MyImage)
                {

                    var uniqueFileName = GetUniqueFileName(item.FileName);
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    item.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                //to do : Save uniqueFileName  to your db table   
            }
            // to do  : Return something
            return RedirectToAction("Index", "Home");
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
