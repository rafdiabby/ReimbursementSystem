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
                return Ok(new ResultVM { Status = (HttpStatusCode.OK).ToString(), Pesan = "Data Berhasil Ditambahkan" });
            }
            if (cek == 2)
            {
                return NotFound(new ResultVM { Status = (HttpStatusCode.NotFound).ToString(), Pesan = "Data Gagal di tambahkan ( NIK Sudah ada ) !!!" });
            }
            if (cek == 3)
            {
                return NotFound(new ResultVM { Status = HttpStatusCode.NotFound.ToString(), Pesan = "Data Gagal Ditambahkan ( Email Sudah di Gunakan ) !!!!" });
            }
            else
            {
                return NotFound(new ResultVM { Status = HttpStatusCode.NotFound.ToString(), Pesan = "Data Gagal Ditambahkan ( No Hp sudah di gunakan )!!!" });
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
        [Route("Search")]
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
    }
}
