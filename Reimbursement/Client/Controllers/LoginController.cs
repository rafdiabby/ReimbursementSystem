using API.ViewModels;
using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : BaseController<LoginVM, LoginRepository, string>
    {
        private readonly LoginRepository repository;
        public LoginController(LoginRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error401()
        {
            return View();
        }
        public IActionResult ErrorNotFound()
        {
            return View();
        }
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.Token;
            var pesan = jwtToken.Messages;

            if (token == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session.SetString("NIK", pesan);
                HttpContext.Session.SetString("JWToken", token);

                return RedirectToAction("Index", "Dashboard");
            }            
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public async Task<JsonResult> Cek(LoginVM login)
        {
            var result = await repository.Cek(login);
            return Json(result);
        }
    }
}
