using System;
using System.Collections.Generic;

namespace FarmManager.Models
{
    public partial class MRabbitsDeathsRecords
    {
        public int Id { get; set; }
        public string NameId { get; set; }
        public DateTime DateOfDeath { get; set; }
        public string Notes { get; set; }
    }
}
