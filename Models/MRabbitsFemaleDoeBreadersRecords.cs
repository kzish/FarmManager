using System;
using System.Collections.Generic;

namespace FarmManager.Models
{
    public partial class MRabbitsFemaleDoeBreadersRecords
    {
        public int Id { get; set; }
        public string NameIdDoe { get; set; }
        public string NameIdBuck { get; set; }
        public DateTime DateMated { get; set; }
        public DateTime? ActualKindlingDate { get; set; }
        public DateTime? ActualWeeningDate { get; set; }
        public int LitterSize { get; set; }
        public int Deaths { get; set; }
        public string Notes { get; set; }
    }
}
