using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Subcate
    {
        public int Subid { get; set; }
        public int Categoryid { get; set; }
        public int? Score { get; set; }
        public string? Label { get; set; }
    }
}
