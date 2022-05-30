using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Category
    {
        public uint Categoryid { get; set; }
        public string Manager { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string Description { get; set; } = null!;
        public uint MaxScore { get; set; }
        public string EvalDateStart { get; set; } = null!;
        public string EvalDateStop { get; set; } = null!;
    }
}
