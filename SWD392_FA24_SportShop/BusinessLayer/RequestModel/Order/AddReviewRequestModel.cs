using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Order
{
    public class AddReviewRequestModel
    {
        public string OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public int ScoreRating { get; set; }
        public string Comment { get; set; }
    }
}
