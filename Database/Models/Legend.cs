using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Legend
    {
        public uint Legendid { get; set; }
        public uint Score { get; set; }
        public uint Cardinal { get; set; }
        public string Name { get; set; } = null!;
    }
}
