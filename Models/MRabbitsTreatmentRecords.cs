using System;
using System.Collections.Generic;

namespace FarmManager.Models
{
    public partial class MRabbitsTreatmentRecords
    {
        public int Id { get; set; }
        public string NameId { get; set; }
        public string Illness { get; set; }
        public string Treatment { get; set; }
        public DateTime DateTreated { get; set; }
        public string Notes { get; set; }
    }
}
