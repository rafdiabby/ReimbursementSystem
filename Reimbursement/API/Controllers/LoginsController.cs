using API.Base;
using API.Repository.Data;
using API.Viewmodels;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : BaseController<LoginVM, LoginRepository, string>
    {
        private readonly LoginRepository loginRepository;
        public IConfiguration _configuration;

        public LoginsController(LoginRepository loginRepository, IConfiguration configuration) : base(loginRepository)
        {
            this.loginRepository = loginRepository;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Sign(LoginVM loginVM)
        {
            var cek = loginRepository.Sign(loginVM);
            if (cek == 2)
            {
                //email tidak terdaftar
                return NotFound(new JWTokenVM { Token = "", Messages = "0" });
            }
            if (cek == 0)
            {
                //password salah
                return NotFound(new JWTokenVM { Token = "", Messages = "1" });
            }
            else
            {
                //memgambil nilai NIK
                var mail = loginVM.Email;
                var nik = loginRepository.CekNIK(mail) ;
                //mengambil rolename dari employe yang berhasil login
                var getRoles = loginRepository.GetRole(loginVM.Email);
                var data = new LoginDataVM()
                {
                    Email = loginVM.Email
                };
                //payload
                var claims = new List<Claim>
                {
                    new Claim("Email", data.Email)
                };
                foreach (var a in getRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, a.ToString()));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Header
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10), //set Experied time
                    signingCredentials: signIn
                    );
                var idtoken = new JwtSecurityTokenHandler().WriteToken(token); //generate token
                claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                return Ok(new JWTokenVM { Token = idtoken, Messages = nik });
            }
        }
    }
}
