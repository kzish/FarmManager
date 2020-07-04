using System;
using System.Collections.Generic;

namespace FarmManager.Models
{
    public partial class MRabbitsDeseasesAndTreatmentNotes
    {
        public int Id { get; set; }
        public string Desease { get; set; }
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
        public string Notes { get; set; }
    }
}
