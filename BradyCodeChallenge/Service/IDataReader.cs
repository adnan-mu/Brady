using BradyCodeChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradyCodeChallenge.Service
{
    internal interface IDataReader
    {
        IEnumerable<WindGenerator>  ReadWindData();
        IEnumerable<GasGenerator> ReadGasData();
        IEnumerable<CoalGenerator> ReadCoalData();
        ReferenceData ReadReferenceData();
    }
}
