using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class User
    {
        public string Userid { get; set; } = null!;
        public int Depid { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public int? Cardinal { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public uint? Roles { get; set; }
    }
}
