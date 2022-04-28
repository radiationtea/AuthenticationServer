using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Legend
    {
        public int Legendid { get; set; }
        public int? Score { get; set; }
        public int? Cardinal { get; set; }
        public string? Name { get; set; }
    }
}
