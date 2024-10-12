﻿using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderId { get; set; }
        public string Date { get; set; }
        public double Amount { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public virtual Order Order { get; set; }
        public virtual User User { get; set; }
    }
}
