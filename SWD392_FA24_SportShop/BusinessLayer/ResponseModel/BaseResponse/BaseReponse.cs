namespace BusinessLayer.ResponseModels
{
    public class BaseResponse<T>
    {
        public int? Code { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
    }

    public class BaseResponse
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }

    public class DynamicResponse<T>
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public PagingMetaData PageInfor { get; set; }
        public List<T> Data { get; set; }
    }

    public class PagingMetaData
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Sort { get; set; }
        public string? Order { get; set; }
        public int TotalPage { get; set; }
        public int TotalItem { get; set; }
    }

    public class PagingModel
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Sort { get; set; }
        public string? Order { get; set; }
    }
}
