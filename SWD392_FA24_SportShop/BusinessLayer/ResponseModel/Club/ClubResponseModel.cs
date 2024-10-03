using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.Club
{
    public class ClubResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime EstablishedYear { get; set; }
        public string StadiumName { get; set; }
        public string? ClubLogo { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}
