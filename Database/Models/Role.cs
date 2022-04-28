using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Role
    {
        public int Roleid { get; set; }
        public uint? Perms { get; set; }
        public string? Label { get; set; }
    }
}
