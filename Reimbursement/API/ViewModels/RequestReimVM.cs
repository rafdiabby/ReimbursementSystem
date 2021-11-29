using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace API.ViewModels
{
    public class RequestReimVM
    {
        public int reimId { get; set; }
        public string NIK { get; set; }
        public DateTime date { get; set; }
        public int amount { get; set; }
        public string description  { get; set; }
        public string[] receipt { get; set; }
        public int categoryId { get; set; }
        public int statusId { get; set; }
        public string statusDetails { get; set; }
        public List<IFormFile> receiptFile { get; set; }


    }
}
