using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class File
    {
        public Guid Fileid { get; set; }
        public uint Postid { get; set; }
        public string? Url { get; set; }
    }
}
