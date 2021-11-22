using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class StatusRepository : GeneralRepository<MyContext, Status, int>
    {
        public StatusRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
