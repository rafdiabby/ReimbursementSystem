using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class CreatePost
    {
        public string ImageCaption { set; get; }
        public string ImageDescription { set; get; }
        public List<IFormFile> MyImage { set; get; }
    }
}
