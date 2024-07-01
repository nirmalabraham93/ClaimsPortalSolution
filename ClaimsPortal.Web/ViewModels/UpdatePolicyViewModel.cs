using System.ComponentModel.DataAnnotations;

namespace ClaimsPortal.Web.ViewModels
{
    public class UpdatePolicyViewModel
    {
        // Policy Holder Fields
        public int PolicyHolderId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        // Policy Fields
        public int PolicyId { get; set; }

        [Required, Range(0, 1000000)]
        public decimal CoverageAmount { get; set; }

        [Required, Range(0, 100000)]
        public decimal PremiumAmount { get; set; }

        // Vehicle Fields
        public int VehicleId { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string EngineNumber { get; set; }

    }
}
