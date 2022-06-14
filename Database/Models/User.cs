using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auth.Database.Models
{
    public partial class User
    {
        [Key]
        public string Userid { get; set; } = null!;
        public uint Depid { get; set; }
        public string Password { get; set; } = null!;
        public string? Salt { get; set; }
        public uint Cardinal { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual Depart Dep { get; set; } = null!;
    }
}
