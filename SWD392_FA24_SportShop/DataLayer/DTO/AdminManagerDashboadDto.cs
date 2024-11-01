using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class AdminManagerDashboadDto
    {
        public int UserCount { get; set; }
        public int ClubCount { get; set; }
        public int SessionCount { get; set; }
        public int PlayerCount { get; set; }
        public int ShirtCount { get; set; }
        public int TypeShirtCount { get; set; }
        public int OrderCount { get; set; }
    }
}
