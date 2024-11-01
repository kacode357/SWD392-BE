using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.Order
{
    public class ReviewResponseModel
    {
        public int OrderDetailId { get; set; }
        public int ScoreRating { get; set; }
        public string Comment { get; set; }
    }
}
