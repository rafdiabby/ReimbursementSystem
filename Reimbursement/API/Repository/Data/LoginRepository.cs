using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class LoginRepository : GeneralRepository<MyContext, LoginVM, string>
    {
        private readonly MyContext myContext;
        public LoginRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Sign(LoginVM loginVM)
        {
            var cekEmail = myContext.Employees.Where(a => a.email == loginVM.Email).FirstOrDefault();
            //Lakukan Cek Email
            if (cekEmail != null)
            {
                //Lakukan cek password jika email ditemukan
                var nik = cekEmail.NIK;
                var getPassword = myContext.Accounts.Find(nik);
                string pass = getPassword.password;
                var cekPassword = Hasing.ValidatePassword(loginVM.Password, pass);
                //Lakukan Cek Password
                if (cekPassword == true)
                {
                    // berhasil login akan me return nilai nik
                    return 1;
                }
                else
                {
                    //jika password salah akan me return hasil nya
                    var result = 0;
                    return result;
                }
            }
            else
            {
                //jika email salah akan me return hasil nya
                var result = 2;
                return result;
            }
        }

        public string CekNIK(string email)
        {
            var cekEmail = myContext.Employees.Where(a => a.email == email).FirstOrDefault();
            var nik = cekEmail.NIK;
            return nik;
        }
        public string[] GetRole(string email)
        {
            var getData = myContext.Employees.Where(a => a.email == email).FirstOrDefault();
            var getRole = (from acr in myContext.AccountRoles
                           join rol in myContext.Roles
                          on acr.roleId equals rol.roleId
                           select new
                           {
                               NIK = acr.NIK,
                               RoleName = rol.roleName
                           }).Where(acr => acr.NIK == getData.NIK).ToList();
            List<string> result = new List<string>();
            foreach (var item in getRole)
            {
                result.Add(item.RoleName);
            }
            return result.ToArray();
        }

        public string Cek(LoginVM loginVM)
        {
            var cekEmail = myContext.Employees.Where(a => a.email == loginVM.Email).FirstOrDefault();
            //Lakukan Cek Email
            if (cekEmail != null)
            {
                //Lakukan cek password jika email ditemukan
                var nik = cekEmail.NIK;
                var getPassword = myContext.Accounts.Find(nik);
                string pass = getPassword.password;
                var cekPassword = Hasing.ValidatePassword(loginVM.Password, pass);
                //Lakukan Cek Password
                if (cekPassword == true)
                {
                    // berhasil login akan me return nilai nik
                    var result = "1";
                    return result;
                }
                else
                {
                    //jika password salah akan me return hasil nya
                    var result = "3";
                    return result;
                }
            }
            else
            {
                //jika email salah akan me return hasil nya
                var result = "2";
                return result;
            }
        }

        public int ResetPW(Account account, string nik)
        {
            Account accounts = new Account()
            {
                NIK = account.NIK,
                password = Hasing.HashPassword(account.password)
            };
            myContext.Entry(accounts).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
