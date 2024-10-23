using BusinessLayer.ResponseModel.ShirtSize;
using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.Shirt
{
    public class ShirtResponseModel
    {
        public int Id { get; set; }
        public int TypeShirtId { get; set; }
        public string TypeShirtName { get; set; }
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public int PlayerId { get; set; }
        public string FullName { get; set; }
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public DateTime ClubEstablishedYear { get; set; }
        public string ClubLogo { get; set; }
        public string ClubCountry { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string UrlImg { get; set; }
        public int Status { get; set; }
        public List<ShirtSizeResponseModel> ListSize { get; set; }

        public static implicit operator ShirtResponseModel(MegaData<ShirtResponseModel> v)
        {
            throw new NotImplementedException();
        }
    }
}
