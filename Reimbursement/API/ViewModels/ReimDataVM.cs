using System;

namespace API.ViewModels
{
    public class ReimDataVM
    {
        public int reimId { get; set; }
        public string NIK { get; set; }
        public string fullName { get; set; }
        public string phone { get; set; }
        public DateTime date { get; set; }
        public int amount { get; set; }
        public string description  { get; set; }
        public string receipt { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        public string statusDetails { get; set; }


    }
}
