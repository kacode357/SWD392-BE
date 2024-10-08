using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class TypeShirtDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public int ClubId { get; set; }
        public string ClubName { get; set; }     
    }
}
