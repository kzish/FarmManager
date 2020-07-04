using System;
using System.Collections.Generic;

namespace FarmManager.Models
{
    public partial class MRabbits
    {
        public int Id { get; set; }
        public string NameId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SireId { get; set; }
        public string DameId { get; set; }
        public int LitterNumber { get; set; }
        public string Notes { get; set; }
    }
}
