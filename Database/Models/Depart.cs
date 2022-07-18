using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auth.Database.Models
{
    public partial class Depart
    {
        [Key]
        public uint Depid { get; set; }
        public string Desc { get; set; } = null!;
    }
}
