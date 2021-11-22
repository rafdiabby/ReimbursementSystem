using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ReimbursementHistoryRepository : GeneralRepository<MyContext, ReimbursementHistory, int>
    {
        public ReimbursementHistoryRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
