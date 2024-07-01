using System.ComponentModel.DataAnnotations;

namespace ClaimsPortal.Web.ViewModels
{
    public class CreatePolicyViewModel
    {
        // Policy Holder Fields
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required, StringLength(10)]
        public string ZIP { get; set; }

        // Policy Fields

        [Required]
        public string PolicyType { get; set; }

        [Required]
        public string PolicyNumber { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime CoverageStartDate { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime CoverageEndDate { get; set; }

        [Required, Range(0, 1000000)]
        public decimal CoverageAmount { get; set; }

        [Required, Range(0, 100000)]
        public decimal PremiumAmount { get; set; }

        // Vehicle Fields

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        public string VIN { get; set; }

        [Required]
        public string EngineNumber { get; set; }

        [Required]
        public string ChasisNumber { get; set; }
    }
}
