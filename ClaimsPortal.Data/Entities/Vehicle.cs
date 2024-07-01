using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsPortal.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string VIN { get; set; }
        public string EngineNumber { get; set; }
        public string ChasisNumber { get; set; }

        // Foreign key
        public int PolicyId { get; set; }

        // Navigation property
        public Policy Policy { get; set; }

    }
}
