﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.OrderDetail
{
    public class GetAllOrderDetailRequestModel
    {
        public int pageNum { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public bool? Status { get; set; }
    }
}
