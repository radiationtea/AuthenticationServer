using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Permission
    {
        public uint Permid { get; set; }
        public string? Label { get; set; }
    }
}
