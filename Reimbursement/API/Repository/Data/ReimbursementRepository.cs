using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ReimbursementRepository : GeneralRepository<MyContext, Reimbursement, int>
    {
        public ReimbursementRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
