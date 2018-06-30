namespace Mpower.Rail.Model.Application
{
    public class ResponseWrapper
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string Status { get; set; }
        public object ResponseResult { get; set; }
    }
}