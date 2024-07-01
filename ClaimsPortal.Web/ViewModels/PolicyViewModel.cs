namespace ClaimsPortal.Web.ViewModels
{
    public class PolicyViewModel
    {
        public int PolicyId { get; set; }
        public string PolicyType { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime CoverageStartDate { get; set; }
        public DateTime CoverageEndDate { get; set; }
        public int PolicyHolderId { get; set; }
        public string PolicyHolderName { get; set; }
        public string PolicyHolderEmail { get; set; }
        public string PolicyHolderAddress { get; set; }
        public int VehicleId { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
    }
}
