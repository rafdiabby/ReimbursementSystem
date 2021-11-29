using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : BaseController<Attachment, AttachmentRepository, int>
    {
        public AttachmentsController(AttachmentRepository attachmentRepository) : base(attachmentRepository)
        {

        }
    }
}
