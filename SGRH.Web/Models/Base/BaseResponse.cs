namespace SGRH.Web.Models.Base
{
    public  abstract class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }
}
