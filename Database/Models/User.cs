using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Auth.Database.Models
{
    public partial class User
    {
        [Key]
        public string Userid { get; set; } = null!;
        public uint Depid { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonIgnore]
        public string Password { get; set; } = null!;
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonIgnore]
        public string? Salt { get; set; }
        public uint Cardinal { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public Depart Dep { get; set; } = null!;
    }
}
