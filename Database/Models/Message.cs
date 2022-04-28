using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Message
    {
        public int Notiid { get; set; }
        public string? Type { get; set; }
        public DateTime Requestedat { get; set; }
        public DateTime? Resolvedat { get; set; }
        public string? Errors { get; set; }
        public string? Phone { get; set; }
        public string? Content { get; set; }
    }
}
