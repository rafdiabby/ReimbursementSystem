using Microsoft.AspNetCore.Authorization;
using API.Models;
using API.ViewModels;
using Client.Models;
using Client.Repository.Data;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class ReimburseController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ReimburseRepository repository;
        private readonly MailRepository mailRepository;
        public ReimburseController(IWebHostEnvironment environment, ReimburseRepository repository, MailRepository mailRepository)
        {
            hostingEnvironment = environment;
            this.repository = repository;
            this.mailRepository = mailRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View(new RequestReimVM());
        }
        public IActionResult Forms()
        {
            return View();
        }
        [Route("Approval/{reimId}")]
        public IActionResult Approval(int reimId)
        {
            var data = new RequestReimVM { reimId = reimId };
            return View(data);
        }
        [Route("Finance/Approval/{reimId}")]
        public IActionResult FinanceApproval(int reimId)
        {
            var data = new RequestReimVM { reimId = reimId };
            return View(data);
        }
        [HttpPost]
        public IActionResult Create(CreatePost model)
        {
            // do other validations on your model as needed
            if (model.MyImage != null)
            {
                var uniqueFileName = GetUniqueFileName(model.MyImage.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, uniqueFileName);
                model.MyImage.CopyTo(new FileStream(filePath, FileMode.Create));

                //to do : Save uniqueFileName  to your db table   
            };
            // to do  : Return something
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult CreateForm(RequestReimVM model)
        {
            // do other validations on your model as needed
            var data = new Reimbursement
            {
                NIK = model.NIK,
                categoryId = model.categoryId,
                description = model.description,
                amount = model.amount,
                statusId = 1,
                date = DateTime.Now,
            };
            var result = repository.RequestReim(data);

            if (model.receiptFile != null)
            {
                foreach (var item in model.receiptFile)
                {

                    var uniqueFileName = GetUniqueFileName(item.FileName);
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    item.CopyTo(new FileStream(filePath, FileMode.Create));
                    var attachment = new Attachment
                    {
                        reimId = repository.GetLastReimId().Result,
                        file = "uploads/" + uniqueFileName
                    };
                    repository.AddAttachment(attachment);
                } 
            }
            var mailContent = new MailRequestVM
            {
                ToEmail = "giribudi30@gmail.com",
                Subject = "New Reimbursement Request",
                Body = "Your employee with NIK : " + data.NIK + " has new reimbursement request waiting for approval, check on website for additional details"
            };
            mailRepository.Send(mailContent);
            return RedirectToAction("Index", "Dashboard");
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        [HttpPatch]
        public ActionResult<Reimbursement> UpdateStatus(Reimbursement reimbursement)
        {
            var result = repository.UpdateStatus(reimbursement);
            return Json(result);
        }
    }
}
