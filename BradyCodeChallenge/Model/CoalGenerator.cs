using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradyCodeChallenge.Model
{
    internal class CoalGenerator : EnergyGenerator
    {
        public decimal TotalHeatInput { get; set; }
        public decimal ActualNetGeneration { get; set; }
        public decimal EmissionsRating { get; set; }
    }
}
