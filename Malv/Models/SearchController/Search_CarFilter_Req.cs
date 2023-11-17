namespace Malv.Models.SearchController
{
    public class Search_CarFilter_Req
	{
		public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? MinMileage { get; set; }
        public int? MaxMileage { get; set; }
        public TransmissionType_Mod? Transmission { get; set; }
        public FuelType_Mod? Fuel { get; set; }
        public CarType_Mod? CarType { get; set; }
        public bool? DriveTrain { get; set; }
    }
}

