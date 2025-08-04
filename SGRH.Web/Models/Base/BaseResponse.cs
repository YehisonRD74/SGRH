namespace SGRH.Web.Models.Base
{
    public abstract class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDisable { get; set; }
    }
}