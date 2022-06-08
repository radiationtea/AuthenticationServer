using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Subcate
    {
        public Subcate()
        {
            Posts = new HashSet<Post>();
        }

        public uint Subid { get; set; }
        public uint Categoryid { get; set; }
        public uint Score { get; set; }
        public string Label { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }
    }
}
