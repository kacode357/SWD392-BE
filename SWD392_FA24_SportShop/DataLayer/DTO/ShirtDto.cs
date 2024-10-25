﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class ShirtDto
    {
        public int Id { get; set; }
        public int TypeShirtId { get; set; }
        public string TypeShirtName { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Name { get; set; } 
        public int? Number { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? UrlImg { get; set; }
        public int Status { get; set; }
    }
}
