using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Size
    {
        public Size()
        {
            ShirtSizes = new HashSet<ShirtSize>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<ShirtSize> ShirtSizes { get; set; }
    }
}
