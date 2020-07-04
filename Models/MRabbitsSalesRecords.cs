using System;
using System.Collections.Generic;

namespace FarmManager.Models
{
    public partial class MRabbitsSalesRecords
    {
        public int Id { get; set; }
        public string NameId { get; set; }
        public DateTime DateSold { get; set; }
        public int WeeksOld { get; set; }
        public decimal WeightKg { get; set; }
        public bool Pedigree { get; set; }
        public decimal Price { get; set; }
        public string BuyerName { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerMobile { get; set; }
        public string Notes { get; set; }
    }
}
