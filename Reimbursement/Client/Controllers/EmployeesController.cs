using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Base.Controllers;
using API.ViewModels;
using Client.Repository.Data;

namespace Client.Controllers
{
    [Authorize]
    //[Authorize(Roles = "HR")]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> Register(RegisterVM register)
        {
            var result = await repository.Register(register);
            return Json(result);
        }
        public JsonResult DeleteEmployees(string id)
        {
            var result = repository.DeleteEmployees(id);
            return Json(result);
        }

        public async Task<JsonResult> GetRole(string id)
        {
            var result = await repository.GetRole(id);
            return Json(result);
        }

        public async Task<JsonResult> AddAccountRole(AccountRole accountRole)
        {
            var result = await repository.AddAccountRole(accountRole);
            return Json(result);
        }
    }
}
