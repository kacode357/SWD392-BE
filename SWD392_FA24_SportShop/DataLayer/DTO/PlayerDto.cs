using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public int? ClubId { get; set; }
        public string ClubName { get; set; }
        public string FullName { get; set; } = null!;
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Nationality { get; set; }
        public bool Status { get; set; }
    }
}
