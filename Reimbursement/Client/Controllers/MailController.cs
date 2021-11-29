using API.ViewModels;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class MailController : Controller
    {
        private readonly MailRepository repository;

        public MailController(MailRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(MailRequestVM mail)
        {
            var result = repository.Send(mail);
            return Json(result);
        }
    }
}
