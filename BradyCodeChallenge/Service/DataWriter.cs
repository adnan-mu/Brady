using BradyCodeChallenge.Constant;
using BradyCodeChallenge.Model;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;

namespace BradyCodeChallenge.Service
{
    internal class DataWriter : IDataWriter
    {
        private readonly IConfiguration configuration;
        private readonly ICalculatorService calculatorService;
        private DirectoryPath directoryPath;

        public DataWriter(IConfiguration configuration, ICalculatorService calculatorService)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            ArgumentNullException.ThrowIfNull(calculatorService);
            this.configuration = configuration;            
            this.calculatorService = calculatorService;
            ReadConfig();
        }

        public void GenerateReport(IEnumerable<WindGenerator> windData, IEnumerable<GasGenerator> gasData, IEnumerable<CoalGenerator> coalData, ReferenceData referenceData)
        {
            XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
            var xml = new XElement(ns + DataConstant.GenerationOutput,
               new XElement(DataConstant.Totals,
               calculatorService.CalculateWindTotal(windData, referenceData),
               calculatorService.CalculateGasTotal(gasData, referenceData),
               calculatorService.CalculateCoalTotal(coalData, referenceData)
                ),
               new XElement(DataConstant.MaxEmissionGenerators,
               calculatorService.CalculateHighestDailyEmission(gasData, coalData, referenceData)               
                ),
               new XElement(DataConstant.ActualHeatRates,
               calculatorService.CalculateActualHeatRate(coalData))
            );
            xml.Save(directoryPath.OutputFile);
        }
        
        private void ReadConfig()
        {
            directoryPath = configuration.GetSection("DirectoryPath").Get<DirectoryPath>();
        }
    }
}
