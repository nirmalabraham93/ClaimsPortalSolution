using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsPortal.Data.Entities
{
    public class Policy
    {
        public int Id { get; set; }      
        public string PolicyType { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime CoverageStartDate { get; set; }
        public DateTime CoverageEndDate { get; set;}
        public decimal CoverageAmount { get; set; }
        public decimal PremiumAmount { get; set;}

        // Foreign key
        public int PolicyHolderId { get; set; }

        // Navigation property
        public PolicyHolder PolicyHolder { get; set; }

        // Navigation property
        public Vehicle Vehicle { get; set; }
    }
}
