using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Club
    {
        public Club()
        {
            ClubPlayers = new HashSet<ClubPlayer>();
            Players = new HashSet<Player>();
            TypeShirts = new HashSet<TypeShirt>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime EstablishedYear { get; set; }
        public string StadiumName { get; set; }
        public string? ClubLogo { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<ClubPlayer> ClubPlayers { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<TypeShirt> TypeShirts { get; set; }
    }
}
