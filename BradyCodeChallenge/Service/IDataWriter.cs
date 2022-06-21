using BradyCodeChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradyCodeChallenge.Service
{
    internal interface IDataWriter
    {
        void GenerateReport(IEnumerable<WindGenerator> windData, 
            IEnumerable<GasGenerator> gasData, 
            IEnumerable<CoalGenerator> coalData,
            ReferenceData referenceData);
    }
}
