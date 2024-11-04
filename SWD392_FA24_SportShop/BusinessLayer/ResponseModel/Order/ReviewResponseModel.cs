using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.Order
{
    public class ReviewResponseModel
    {
        //public int userId {  get; set; }
        public string UserName { get; set; }
        public string? ImgUrl { get; set; }

        public int OrderDetailId { get; set; }
        public int ScoreRating { get; set; }
        public string Comment { get; set; }
    }
}
