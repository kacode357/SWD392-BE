using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Session
    {
        public Session()
        {
            TypeShirts = new HashSet<TypeShirt>();
        }

        public int Id { get; set; }
        public string Name { get; set; } 
        public DateTime StartDdate { get; set; }
        public DateTime EndDdate { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<TypeShirt> TypeShirts { get; set; }
    }
}
