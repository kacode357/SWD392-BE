using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class ClubPlayer
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public int PlayerId { get; set; }
        public string? Description { get; set; }

        public virtual Club Club { get; set; }
        public virtual Player Player { get; set; }
    }
}
