using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Role
    {
        public uint Roleid { get; set; }
        public string Userid { get; set; } = null!;
        public string? Label { get; set; }
    }
}
