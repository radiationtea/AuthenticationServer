using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class History
    {
        public int Hisid { get; set; }
        public int Subid { get; set; }
        public int Categoryid { get; set; }
        public string Teacherid { get; set; } = null!;
        public string Userid { get; set; } = null!;
        public DateTime? Createdat { get; set; }
    }
}
