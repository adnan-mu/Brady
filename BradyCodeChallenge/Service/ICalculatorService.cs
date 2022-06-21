
using BradyCodeChallenge.Model;
using System.Xml.Linq;

namespace BradyCodeChallenge.Service
{
    internal interface ICalculatorService
    {
        IEnumerable<XElement> CalculateCoalTotal(IEnumerable<CoalGenerator> coalData, ReferenceData referenceData);
        IEnumerable<XElement> CalculateGasTotal(IEnumerable<GasGenerator> gasData, ReferenceData referenceData);
        IEnumerable<XElement> CalculateWindTotal(IEnumerable<WindGenerator> windData, ReferenceData referenceData);
        IEnumerable<XElement> CalculateHighestDailyEmission(IEnumerable<GasGenerator> gasData, IEnumerable<CoalGenerator> coalData, ReferenceData referenceData);
        IEnumerable<XElement> CalculateActualHeatRate(IEnumerable<CoalGenerator> coalData);
    }
}
