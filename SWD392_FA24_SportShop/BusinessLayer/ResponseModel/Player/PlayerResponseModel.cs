using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.Player
{
    public class PlayerResponseModel
    {
        public int Id { get; set; }
        public int? ClubId { get; set; }
        public string FullName { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public DateTime? Birthday { get; set; }
        public string Nationality { get; set; }
        public bool Status { get; set; }
    }
}
