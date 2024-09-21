using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }

        public virtual User User { get; set; }
    }
}
