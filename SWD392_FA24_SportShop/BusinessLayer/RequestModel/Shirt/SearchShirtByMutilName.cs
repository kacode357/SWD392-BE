using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Shirt
{
    public class SearchShirtByMutilName
    {
        public int pageNum { get; set; } = 1;
        public int pageSize { get; set; } = 1;
        public string? nameShirt { get; set; }
        public string? nameClub { get; set; }
        public string? nameSeason { get; set; }
        public string? namePlayer{ get; set; }
        public int? status { get; set; }
    }
}
