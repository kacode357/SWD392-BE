using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class TypeShirt
    {
        public TypeShirt()
        {
            Shirts = new HashSet<Shirt>();
        }

        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ClubId { get; set; }
        public string Name { get; set; } 
        public string? Description { get; set; }
        public bool Status { get; set; }

        public virtual Club Club { get; set; } 
        public virtual Session Session { get; set; } 
        public virtual ICollection<Shirt> Shirts { get; set; }
    }
}
