using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class StatusHistoryRepository : GeneralRepository<MyContext, StatusHistory, int>
    {
        public StatusHistoryRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
