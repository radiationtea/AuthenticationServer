using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Depart
    {
        public uint Depid { get; set; }
        public string Desc { get; set; } = null!;
    }
}
