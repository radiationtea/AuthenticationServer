using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Auth.Database.Models
{
    public partial class User
    {
        [Key]
        public string Userid { get; set; } = null!;
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string Password { get; set; } = null!;
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string? Salt { get; set; }
        public uint Cardinal { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public uint Depid { get; set; }
        [ForeignKey("Depid")]
        public virtual Depart Dep { get; set; } = null!;
    }
}
