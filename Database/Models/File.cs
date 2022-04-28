using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class File
    {
        public Guid Fileid { get; set; }
        public int Postid { get; set; }
        public string Userid { get; set; } = null!;
        public string? Url { get; set; }
    }
}
