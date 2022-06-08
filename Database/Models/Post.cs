using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Post
    {
        public uint Postid { get; set; }
        public string Userid { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime? Createdat { get; set; }
        public uint Subid { get; set; }
        public bool Closed { get; set; }

        public virtual Subcate Sub { get; set; } = null!;
    }
}
