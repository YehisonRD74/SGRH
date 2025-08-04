namespace SGRH.Web.Models.Floor
{
    public class GetAllFloorResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<FloorModels> Data { get; set; }
    }
}
