using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class CategoryRepository : GeneralRepository<MyContext, Category, int>
    {
        public CategoryRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
