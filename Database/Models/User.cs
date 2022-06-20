using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Auth.Database.Models
{
    public partial class User
    {
        public string Userid { get; set; } = null!;
        public uint Depid { get; set; }
        [JsonIgnore]
        public string Password { get; set; } = null!;
        public string? Salt { get; set; }
        public uint Cardinal { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual Depart Dep { get; set; } = null!;
    }
}
