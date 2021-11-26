using API.Context;
using API.Models;
using API.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            var cekNIK = myContext.Employees.Find(registerVM.NIK);
            var cekEmail = myContext.Employees.Where(a => a.email == registerVM.email).FirstOrDefault();
            var cekPhone = myContext.Employees.Where(b => b.phone == registerVM.phone).FirstOrDefault();

            if (cekNIK != null)
            {
                //Jika nik sama
                return 2;
            }
            else if (cekEmail != null)
            {
                //jika email sama
                return 3;
            }
            else if (cekPhone != null)
            {
                //jika nomer sama
                return 4;
            }
            else
            {
                Employee employee = new Employee()
                {
                    NIK = registerVM.NIK,
                    firstName = registerVM.firstName,
                    lastName = registerVM.lastName,
                    phone = registerVM.phone,
                    email = registerVM.email,
                    gender = (Models.Gender)registerVM.gender,
                    bankAccount = registerVM.bankAccount
                };
                //insert data ke tabel employee
                myContext.Employees.Add(employee);
                myContext.SaveChanges();

                Account account = new Account()
                {
                    NIK = registerVM.NIK,
                    password = Hasing.HashPassword(registerVM.Password)
                };
                //insert data ke tabel Account
                myContext.Accounts.Add(account);
                myContext.SaveChanges();

                int cekRole = registerVM.roleId;
                int roleId;
                if (cekRole == 0)
                {
                    roleId = 1;
                }
                else
                {
                    roleId = cekRole;
                }

                AccountRole accountRole = new AccountRole()
                {
                    NIK = employee.NIK,
                    roleId = roleId
                };
                myContext.AccountRoles.Add(accountRole);
                var result = myContext.SaveChanges();
                return result;
            }
        }

        public IEnumerable<ProfilVM> GetProfile()
        {
            var join = (from em in myContext.Employees
                        join ac in myContext.Accounts
                         on em.NIK equals ac.NIK
                        join acr in myContext.AccountRoles
                         on ac.NIK equals acr.NIK
                        join role in myContext.Roles
                         on acr.roleId equals role.roleId
                        select new ProfilVM
                        {
                            NIK = em.NIK,
                            fullName = $"{em.firstName} {em.lastName}",
                            phone = em.phone,
                            email = em.email,
                            gender = em.gender,
                            roleName = role.roleName,
                        });
            return join.ToList();
        }

        public IEnumerable<ProfilVM> GetProfile(string NIK)
        {
            var profile = (from em in myContext.Employees
                        join ac in myContext.Accounts
                         on em.NIK equals ac.NIK
                        join acr in myContext.AccountRoles
                         on ac.NIK equals acr.NIK
                        join role in myContext.Roles
                         on acr.roleId equals role.roleId
                        select new ProfilVM
                        {
                            NIK = em.NIK,
                            fullName = $"{em.firstName} {em.lastName}",
                            phone = em.phone,
                            email = em.email,
                            gender = em.gender,
                            roleName = role.roleName,
                        }).Where(em => em.NIK == NIK).ToList();
            return profile;
        }

        public int Hapus(string NIK)
        {
            var acr = myContext.AccountRoles.Where(a => a.NIK == NIK).ToList();
            foreach(var ac in acr)
            {
                var hapusAccoutRole = myContext.AccountRoles.Find(ac.accountRoleId);
                myContext.Remove(hapusAccoutRole);
                myContext.SaveChanges();
            }            

            var entity = myContext.Employees.Find(NIK);
            myContext.Remove(entity);
            var result = myContext.SaveChanges();
            return result;
        }
        public IEnumerable<GetRoleVM> GetRole(string NIK)
        {
            var profile = (from acr in myContext.AccountRoles
                           join role in myContext.Roles
                            on acr.roleId equals role.roleId
                           select new GetRoleVM
                           {
                               accountRoleid = acr.accountRoleId,
                               nik = acr.NIK,
                               roleName = role.roleName,
                           }).Where(em => em.nik == NIK).ToList();
            return profile;
        }

        public int AddAccountRole(AccountRole accountRole)
        {
            var cekRole = myContext.AccountRoles.Where(a => (a.NIK == accountRole.NIK)&(a.roleId == accountRole.roleId)).FirstOrDefault();
            if (cekRole != null)
            {
                //Jika nik sama
                return 2;
            }
            else
            {
                myContext.AccountRoles.Add(accountRole);
                var result = myContext.SaveChanges();
                return result;
            }
        }
    }
}
