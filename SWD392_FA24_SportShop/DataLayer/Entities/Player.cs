﻿using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Player
    {
        public Player()
        {
            ClubPlayers = new HashSet<ClubPlayer>();
            Shirts = new HashSet<Shirt>();
        }

        public int Id { get; set; }
        public int? ClubId { get; set; }
        public string FullName { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Nationality { get; set; }
        public bool Status { get; set; }

        public virtual Club? Club { get; set; }
        public virtual ICollection<ClubPlayer> ClubPlayers { get; set; }
        public virtual ICollection<Shirt> Shirts { get; set; }
    }
}
