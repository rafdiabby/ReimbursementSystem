using API.Models;
using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository repository;
        public AccountRolesController(AccountRoleRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
