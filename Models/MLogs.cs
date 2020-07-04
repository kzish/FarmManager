using System;
using System.Collections.Generic;

namespace FarmManager.Models
{
    public partial class MLogs
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }
    }
}
