using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Message
    {
        public uint Msgid { get; set; }
        public string Type { get; set; } = null!;
        public DateTime Requestedat { get; set; }
        public DateTime? Resolvedat { get; set; }
        public string? Errors { get; set; }
        public string Content { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
