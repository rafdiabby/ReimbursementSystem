using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ReimbursementRepository : GeneralRepository<MyContext, Reimbursement, int>
    {
        private readonly MyContext context;
        public ReimbursementRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public IEnumerable<ReimDataVM> ReimData()
        {
            var employeeData = context.Employees.ToList();
            var reimData = context.Reimbursements.ToList();
            var statusData = context.Statuses.ToList();
            var categoryData = context.Categories.ToList();

            var data = from e in employeeData
                       join r in reimData on e.NIK equals r.NIK into table0

                       from r in table0.ToList()
                       join s in statusData on r.statusId equals s.id
                       join c in categoryData on r.categoryId equals c.id into table1

                       from c in table1
                       select new ReimDataVM
                       {
                           reimId = r.id,
                           NIK = e.NIK,
                           fullName = e.firstName + " " + e.lastName,
                           phone = e.phone,
                           date = r.date,
                           amount = r.amount,
                           description = r.description,
                           receipt = GetAttachment(r.id),
                           category = c.categoryName,
                           status = s.statusName,
                           statusDetails = r.statusDetails
                       };
            return data.ToList();
        }

        public IEnumerable<ReimDataVM> ReimDataBy(string NIK)
        {
            var employeeData = context.Employees.ToList();
            var reimData = context.Reimbursements.ToList();
            var statusData = context.Statuses.ToList();
            var categoryData = context.Categories.ToList();

            var data = from e in employeeData
                       join r in reimData on e.NIK equals r.NIK into table0

                       from r in table0.ToList()
                       join s in statusData on r.statusId equals s.id
                       join c in categoryData on r.categoryId equals c.id into table1

                       from c in table1
                       where e.NIK == NIK
                       select new ReimDataVM
                       {
                           reimId = r.id,
                           NIK = e.NIK,
                           fullName = e.firstName + " " + e.lastName,
                           phone = e.phone,
                           date = r.date,
                           amount = r.amount,
                           description = r.description,
                           receipt = GetAttachment(r.id),
                           category = c.categoryName,
                           status = s.statusName,
                           statusDetails = r.statusDetails
                       };
            return data.ToList();
        }
        public IEnumerable<ReimDataVM> ReimDataId(int Id)
        {
            var employeeData = context.Employees.ToList();
            var reimData = context.Reimbursements.ToList();
            var statusData = context.Statuses.ToList();
            var categoryData = context.Categories.ToList();

            var data = from e in employeeData
                       join r in reimData on e.NIK equals r.NIK into table0

                       from r in table0.ToList()
                       join s in statusData on r.statusId equals s.id
                       join c in categoryData on r.categoryId equals c.id into table1

                       from c in table1
                       where r.id == Id
                       select new ReimDataVM
                       {
                           reimId = r.id,
                           NIK = e.NIK,
                           fullName = e.firstName + " " + e.lastName,
                           phone = e.phone,
                           date = r.date,
                           amount = r.amount,
                           description = r.description,
                           receipt = GetAttachment(r.id),
                           category = c.categoryName,
                           status = s.statusName,
                           statusDetails = r.statusDetails
                       };
            return data.ToList();
        }

        public int UpdateStatus(Reimbursement reimbursement)
        {
            var checkReimId = context.Reimbursements.Find(reimbursement.id);
            checkReimId.statusId = reimbursement.statusId;
            checkReimId.statusDetails = reimbursement.statusDetails;

            context.Entry(checkReimId).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result ;
        }

        public string[] GetAttachment(int reimId)
        {
            var attachment = context.Attachments.Where(fn => fn.reimId == reimId).ToList();
            List<string> result = new List<string>();
            foreach (var item in attachment)
            {
                result.Add(item.file);
            }

            return result.ToArray();
        }

        //ambil id terakhir
        public int LastId()
        {
            var result = context.Reimbursements.OrderByDescending(r => r.id).FirstOrDefault().id;
            return result;
        }
    }
}
