﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.TypeShirt
{
    public class CreateTypeShirtRequestModel
    {
        public int SessionId { get; set; }
        public int ClubId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}
