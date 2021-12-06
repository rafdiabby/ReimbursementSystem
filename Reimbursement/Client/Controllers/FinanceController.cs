using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class FinanceController : Controller
    {
        [Authorize(Roles="Finance")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
