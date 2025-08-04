namespace SGRH.Web.Models.Floor
{
    public class GetFloorResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public FloorModels data { get; set; }
    }
}
