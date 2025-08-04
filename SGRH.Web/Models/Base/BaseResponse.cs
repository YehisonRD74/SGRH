namespace SGRH.Web.Models.Base
{
    public  abstract class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDisable { get; set; }


        public BaseResponse(bool isSuccess, string message, string updatedBy, DateTime updatedAt, bool isDisable =false)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.UpdatedBy = string.Empty;
            this.UpdatedAt = DateTime.Now;
            this.IsDisable = isDisable;
        }


    }
   
}
