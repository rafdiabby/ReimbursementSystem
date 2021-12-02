using API.Base;
using API.Models;
using API.Repository.Data;
using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly MailServices mailServices;
        public IConfiguration _configuration;
        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration, MailServices mailServices) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this._configuration = configuration;
            this.mailServices = mailServices;
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            int length = 7;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            var pass = str_build.ToString();

            RegisterVM dataRegister = new RegisterVM()
            {
                NIK = registerVM.NIK,
                firstName = registerVM.firstName,
                lastName = registerVM.lastName,
                email = registerVM.email,
                phone = registerVM.phone,
                bankAccount = registerVM.bankAccount,
                gender = registerVM.gender,
                Password = pass,
                roleId = registerVM.roleId,
            };
            var cek = employeeRepository.Register(dataRegister);
            if (cek == 1)
            {
                MailRequestVM mail = new MailRequestVM()
                {
                    ToEmail = "giribudi30@gmail.com",
                    Subject = "Registrasi Akun",
                    Body = $"Akun berhasil di buat, silahkan Login ke aplikasi dengan password : {pass} dan lakukan perubahan password di menu setting :)",
                    Attachments = null
                };

                _ = mailServices.SendEmailAsync(mail);

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

        [HttpGet]
        [Route("GetRole")]
        public ActionResult<GetRoleVM> GetRole(string NIK)
        {
            var result = employeeRepository.GetRole(NIK);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddAccountRole")]
        public ActionResult AddAccountRole(AccountRole accountRole)
        {
            var cek = employeeRepository.AddAccountRole(accountRole);
            if (cek == 1)
            {
                return Ok(new ResultVM { Status = (HttpStatusCode.OK).ToString(), Pesan = "1" });
            }
            else
            {
                //Phone
                return NotFound(new ResultVM { Status = HttpStatusCode.NotFound.ToString(), Pesan = "2" });
            }
        }
    }
}
