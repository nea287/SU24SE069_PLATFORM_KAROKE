﻿using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Package
    {
        public Guid PackageId { get; set; }
        public string PackageName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal MoneyAmount { get; set; }
        public int StarNumber { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatorId { get; set; }
    }
}
