﻿using System;
using System.Collections.Generic;

namespace Auth.Database.Models
{
    public partial class Role
    {
        public string Userid { get; set; } = string.Empty;
        public string? Label { get; set; }
        public uint Roleid { get; set; }
    }
}
