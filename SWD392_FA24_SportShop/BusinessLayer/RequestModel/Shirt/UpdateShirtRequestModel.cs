﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Shirt
{
    public class UpdateShirtRequestModel
    {
        public int TypeShirtId { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? UrlImg { get; set; }
        public int Status { get; set; }
    }
}
