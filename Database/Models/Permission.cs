using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Permission
    {
        public uint Permid { get; set; }
        public uint Roleid { get; set; }
        public string Label { get; set; } = null!;
    }
}
