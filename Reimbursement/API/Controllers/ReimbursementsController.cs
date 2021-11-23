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
        public ActionResult<RequestReim> GetAll()
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
        public ActionResult<RequestReim> GetBy(string NIK)
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

        [Route("UpdateStatus")]
        [HttpPatch]
        public IActionResult UpdateStatus(Reimbursement reimbursement)
        {
            var result = reimbursementRepository.UpdateStatus(reimbursement);
            return Ok(result);
        }
    }
}
