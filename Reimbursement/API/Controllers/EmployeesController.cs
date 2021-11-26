using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public IConfiguration _configuration;
        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var cek = employeeRepository.Register(registerVM);
            if (cek == 1)
            {
                return Ok(new ResultVM { Status = (HttpStatusCode.OK).ToString(), Pesan = "1" });
            }
            if (cek == 2)
            {
                //NIK
                return NotFound(new ResultVM { Status = (HttpStatusCode.NotFound).ToString(), Pesan = "2" });
            }
            if (cek == 3)
            {
                //Email
                return NotFound(new ResultVM { Status = HttpStatusCode.NotFound.ToString(), Pesan = "3" });
            }
            else
            {
                //Phone
                return NotFound(new ResultVM { Status = HttpStatusCode.NotFound.ToString(), Pesan = "4" });
            }
        }

        [HttpGet]
        [Route("Profile")]
        public ActionResult<ProfilVM> GetProfile()
        {
            var result = employeeRepository.GetProfile();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(new ResultVM{ Status = HttpStatusCode.NotFound.ToString(), Pesan = "Data Tidak ada !!!" });
            }
        }

        //[Route("Profile/{nik}")]
        //[HttpGet("{nik}")]
        [HttpGet]
        [Route("Profile/{NIK}")]
        public ActionResult<ProfilVM> GetProfile(string NIK)
        {
            var result = employeeRepository.GetProfile(NIK);
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(new ResultVM{ Status = HttpStatusCode.NotFound.ToString(), Pesan = "Data tidak ditemukan !!!" });
            }
        }

        [HttpDelete]
        [Route("Hapus")]
        public ActionResult Hapus(string NIK)
        {
            try
            {
                employeeRepository.Hapus(NIK);
                //Berhasil Hapus
                return Ok(new ResultVM { Status = HttpStatusCode.OK.ToString(), Pesan ="1" });
            }
            catch (Exception e)
            {
                //Gagal 
                return NotFound(new ResultVM { Status = HttpStatusCode.NotFound.ToString(), Pesan = e.ToString() });
            }
        }
    }
}
