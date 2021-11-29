using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class AttachmentRepository : GeneralRepository<MyContext, Attachment, int>
    {
        public AttachmentRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
