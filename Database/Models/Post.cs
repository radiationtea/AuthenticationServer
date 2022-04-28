using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Post
    {
        public int Postid { get; set; }
        public int Categoryid { get; set; }
        public string Userid { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime? Createdat { get; set; }
    }
}
