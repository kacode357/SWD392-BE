using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Session
{
    public class CreateSessionRequestModel
    {
        public string Name { get; set; }
        public DateTime StartDdate { get; set; }
        public DateTime EndDdate { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
