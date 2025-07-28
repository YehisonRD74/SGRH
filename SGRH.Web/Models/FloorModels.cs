namespace SGRH.Web.Models
{
    public class FloorModels

       

    {
        public int floorNumber { get; set; }
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        public string updatedBy { get; set; }
        public bool isDeleted { get; set; }
        public string deletedBy { get; set; }
        public DateTime? deletedAt
        { get; set; }

    }

	public class GetAllFloorResponse
	{
		public bool isSuccess { get; set; }
		public string message { get; set; }
		public List<FloorModels> data { get; set; }
	}

	public class GetFloorResponse
	{
		public bool isSuccess { get; set; }
		public string message { get; set; }
		public FloorModels data { get; set; }
	}


}

