using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Subcate
    {
        public uint Subid { get; set; }
        public uint Categoryid { get; set; }
        public uint Score { get; set; }
    }
}
