using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReimbursementsController : BaseController<Reimbursement, ReimbursementRepository, int>
    {
        private readonly ReimbursementRepository reimbursementRepository;
        public ReimbursementsController(ReimbursementRepository reimbursementRepository) : base(reimbursementRepository)
        {
            this.reimbursementRepository = reimbursementRepository;
        }

        [Route("GetAll")]
        [HttpGet]
        public ActionResult<ReimDataVM> GetAll()
        {
            var data = reimbursementRepository.ReimData();
            if (data.Count() == 0)
            {
                return Ok(new { message = "Can't find data" });
            }
            else
            {
            return Ok(data);
            }
            
        }

        [Route("GetAll/{NIK}")]
        [HttpGet]
        public ActionResult<ReimDataVM> GetBy(string NIK)
        {
            var data = reimbursementRepository.ReimDataBy(NIK);
            if (data.Count() == 0)
            {
                return Ok(new { message = "Can't find data" });
            }
            else
            {
                return Ok(data);
            }
        }
        [Route("GetOnly/{id}")]
        [HttpGet]
        public ActionResult<ReimDataVM> GetOnly(int id)
        {
            var data = reimbursementRepository.ReimDataId(id);
            if (data.Count() == 0)
            {
                return BadRequest(new { message = "Can't find data" });
            }
            else
            {
                return Ok(data);
            }

        }

        [Route("UpdateStatus")]
        [HttpPatch]
        public IActionResult UpdateStatus(Reimbursement reimbursement)
        {
            var result = reimbursementRepository.UpdateStatus(reimbursement);
            return Ok(result);
        }
        [Route("LastId")]
        [HttpGet]
        public IActionResult GetLastId()
        {
            return Ok(reimbursementRepository.LastId());
        }
        [Route("Check/{id}")]
        [HttpGet]
        public IActionResult Check(int id)
        {
            return Ok(reimbursementRepository.CheckReimburse(id));
        }
    }
}
