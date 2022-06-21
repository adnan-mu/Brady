using BradyCodeChallenge.Constant;
using BradyCodeChallenge.Model;
using System.Xml.Linq;

namespace BradyCodeChallenge.Service
{
    public class CalculatorService : ICalculatorService   
    {
        public IEnumerable<XElement> CalculateCoalTotal(IEnumerable<CoalGenerator> coalData, ReferenceData referenceData)
        {
            return coalData.Select(coal =>
                            new XElement(DataConstant.Generator,
                                new XElement(DataConstant.Name, coal.Name),
                                new XElement(DataConstant.Total, coal.Generation.Select(day => CalculateDailyGenerationValueForGasAndCoal(day.Energy, day.Price, referenceData)).Sum()))
                               );
        }

        public IEnumerable<XElement> CalculateGasTotal(IEnumerable<GasGenerator> gasData, ReferenceData referenceData)
        {
            return gasData.Select(gas =>
                            new XElement(DataConstant.Generator,
                                new XElement(DataConstant.Name, gas.Name),
                                new XElement(DataConstant.Total, gas.Generation.Select(day => CalculateDailyGenerationValueForGasAndCoal(day.Energy, day.Price, referenceData)).Sum()))
                               );
        }

        public IEnumerable<XElement> CalculateWindTotal(IEnumerable<WindGenerator> windData, ReferenceData referenceData)
        {
            return windData.Select(wind =>
                            new XElement(DataConstant.Generator,
                                new XElement(DataConstant.Name, wind.Name),
                                new XElement(DataConstant.Total, wind.Generation.Select(day => CalculateDailyGenerationValueForWind(day.Energy, day.Price, wind.Location, referenceData)).Sum()))
                               );
        }

        public IEnumerable<XElement> CalculateActualHeatRate(IEnumerable<CoalGenerator> coalData)
        {
            return coalData.Select(coal =>
                            new XElement(DataConstant.ActualHeatRate,
                                new XElement(DataConstant.Name, coal.Name),
                                new XElement(DataConstant.HeatRate, CalculateHeatRate(coal)))
                               );
        }

        public IEnumerable<XElement> CalculateHighestDailyEmission(IEnumerable<GasGenerator> gasData, IEnumerable<CoalGenerator> coalData, ReferenceData referenceData)
        {
            var dailyEmission = new Dictionary<DateTime, MaxEmissionData>();
            foreach (var gas in gasData)
            {
                foreach (var day in gas.Generation)
                {
                    dailyEmission.Add(day.Date, new MaxEmissionData { Emissions = CalcualteDialyEmissions(referenceData, gas, day), Name = gas.Name });
                }
            }

            foreach (var coal in coalData)
            {
                foreach (var day in coal.Generation)
                {
                    if (dailyEmission.ContainsKey(day.Date))
                    {
                        var dayEmission = CalcualteDialyEmissions(referenceData, coal, day);
                        if (dailyEmission[day.Date].Emissions < dayEmission)
                        {
                            dailyEmission[day.Date].Emissions = dayEmission;
                            dailyEmission[day.Date].Name = coal.Name;
                        }
                    }
                    else
                    {
                        dailyEmission.Add(day.Date, new MaxEmissionData { Emissions = CalcualteDialyEmissions(referenceData, coal, day), Name = coal.Name });
                    }
                }
            }

            return dailyEmission.Select(de =>
                            new XElement(DataConstant.Day,
                                new XElement(DataConstant.Name, de.Value.Name),
                                new XElement(DataConstant.Date, de.Key),
                                new XElement(DataConstant.Emission, de.Value.Emissions)
                               ));
        }

        private static decimal CalculateHeatRate(CoalGenerator coal)
        {
            return coal.TotalHeatInput / coal.ActualNetGeneration;
        }

        

        private decimal CalcualteDialyEmissions(ReferenceData referenceData, GasGenerator gas, Day gasGen)
        {
            return gasGen.Energy * gas.EmissionsRating * referenceData.emissionFactor.Medium;
        }

        private decimal CalcualteDialyEmissions(ReferenceData referenceData, CoalGenerator coal, Day coalGen)
        {
            return coalGen.Energy * coal.EmissionsRating * referenceData.emissionFactor.High;
        }       

        private decimal CalculateDailyGenerationValueForWind(decimal energy, decimal price, string location, ReferenceData referenceData) =>
            location switch
            {
                DataConstant.Offshore => energy * price * referenceData.valueFactor.Low,
                DataConstant.Onshore => energy * price * referenceData.valueFactor.High,
                _ => throw new ArgumentOutOfRangeException(nameof(location), $"Not expected direction value: {location}"),
            };

        private decimal CalculateDailyGenerationValueForGasAndCoal(decimal energy, decimal price, ReferenceData referenceData) => energy * price * referenceData.valueFactor.Medium;


    }
}
