using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Category
    {
        public int Categoryid { get; set; }
        public string? Description { get; set; }
        public int? MaxScore { get; set; }
        public string? Manager { get; set; }
        public string? EvalDateStart { get; set; }
        public string? EvalDateStop { get; set; }
        public string? Label { get; set; }
    }
}
