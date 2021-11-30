using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class FinanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
