using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<RequestReim> ReimData()
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
                       select new
                       {
                           reimId = r.id,
                           NIK = e.NIK,
                           fullName = e.firstName + " " + e.lastName,
                           phone = e.phone,
                           date = r.date,
                           amount = r.amount,
                           description = r.description,
                           receipt = r.receipt,
                           category = c.categoryName,
                           status = s.statusName,
                           statusDetails = r.statusDetails
                       };
            return (IEnumerable<RequestReim>)data.ToList();
        }

        public IEnumerable<RequestReim> ReimDataBy(string NIK)
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
                       select new
                       {
                           reimId = r.id,
                           NIK = e.NIK,
                           fullName = e.firstName + " " + e.lastName,
                           phone = e.phone,
                           date = r.date,
                           amount = r.amount,
                           description = r.description,
                           receipt = r.receipt,
                           category = c.categoryName,
                           status = s.statusName,
                           statusDetails = r.statusDetails
                       };
            return (IEnumerable<RequestReim>)data.ToList();
        }

        public int UpdateStatus(Reimbursement reimbursement)
        {
            var checkReimId = context.Reimbursements.Find(reimbursement.id);
            checkReimId.Status = reimbursement.Status;
            checkReimId.statusDetails = reimbursement.statusDetails;

            context.Entry(checkReimId).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result ;
        }
    }
}
