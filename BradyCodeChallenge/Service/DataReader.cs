using BradyCodeChallenge.Constant;
using BradyCodeChallenge.Model;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;

namespace BradyCodeChallenge.Service
{
    internal class DataReader : IDataReader
    {
        private IConfiguration configuration;
        private DirectoryPath directoryPath;
        private XElement parentNode;
        private XElement referenceNode;

        public DataReader(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            this.configuration = configuration;
            ReadConfig();
            parentNode = XElement.Load(directoryPath.InputFile);
            referenceNode = XElement.Load(directoryPath.ReferenceFile);
        }

        public IEnumerable<WindGenerator> ReadWindData()
        {        
            var windGenerator = parentNode.Element(DataConstant.Wind).Elements(DataConstant.WindGenerator).
                Select(generator =>
                   new WindGenerator
                   {
                       Name = generator.Element(DataConstant.Name).Value,
                       Location = generator.Element(DataConstant.Location).Value,
                       Generation = generator.Element(DataConstant.Generation).Elements(DataConstant.Day).
                       Select(day => new Day
                       {
                           Date = ((DateTime)day.Element(DataConstant.Date)),
                           Energy = ((decimal)day.Element(DataConstant.Energy)),
                           Price = ((decimal)day.Element(DataConstant.Price))
                       }).ToList(),
                   }
                   ).ToList();

            return windGenerator;
        }

        public IEnumerable<GasGenerator> ReadGasData()
        {
            var gasGenerator = parentNode.Element(DataConstant.Gas).Elements(DataConstant.GasGenerator).
                Select(generator =>
                   new GasGenerator
                   {
                       Name = generator.Element(DataConstant.Name).Value,
                       EmissionsRating = (decimal)generator.Element(DataConstant.EmissionsRating),
                       Generation = generator.Element(DataConstant.Generation).Elements(DataConstant.Day).
                       Select(day => new Day
                       {
                           Date = ((DateTime)day.Element(DataConstant.Date)),
                           Energy = ((decimal)day.Element(DataConstant.Energy)),
                           Price = ((decimal)day.Element(DataConstant.Price))
                       }).ToList(),
                   }
                   ).ToList();

            return gasGenerator;
        }

        public IEnumerable<CoalGenerator> ReadCoalData()
        {            
            var coalGenerator = parentNode.Element(DataConstant.Coal).Elements(DataConstant.CoalGenerator).
                Select(generator =>
                   new CoalGenerator
                   {
                       Name = generator.Element(DataConstant.Name).Value,
                       TotalHeatInput = (decimal)generator.Element(DataConstant.TotalHeatInput),
                       ActualNetGeneration = (decimal)generator.Element(DataConstant.ActualNetGeneration),
                       EmissionsRating = (decimal)generator.Element(DataConstant.EmissionsRating),
                       Generation = generator.Element(DataConstant.Generation).Elements(DataConstant.Day).
                       Select(day => new Day
                       {
                           Date = ((DateTime)day.Element(DataConstant.Date)),
                           Energy = ((decimal)day.Element(DataConstant.Energy)),
                           Price = ((decimal)day.Element(DataConstant.Price))
                       }).ToList(),
                   }
                   ).ToList();

            return coalGenerator;
        }

        public ReferenceData ReadReferenceData()
        {
            
            ReferenceData referenceData = new ReferenceData {
                emissionFactor = new FactorData {
                    High = (decimal)referenceNode.Element(DataConstant.Factors).Element(DataConstant.EmissionsFactor).Element(DataConstant.High),
                    Medium = (decimal)referenceNode.Element(DataConstant.Factors).Element(DataConstant.EmissionsFactor).Element(DataConstant.Medium),
                    Low = (decimal)referenceNode.Element(DataConstant.Factors).Element(DataConstant.EmissionsFactor).Element(DataConstant.Low),
                },
                valueFactor = new FactorData
                {
                    High = (decimal)referenceNode.Element(DataConstant.Factors).Element(DataConstant.ValueFactor).Element(DataConstant.High),
                    Medium = (decimal)referenceNode.Element(DataConstant.Factors).Element(DataConstant.ValueFactor).Element(DataConstant.Medium),
                    Low = (decimal)referenceNode.Element(DataConstant.Factors).Element(DataConstant.ValueFactor).Element(DataConstant.Low),
                }
            };
            return referenceData;
        }

        private void ReadConfig()
        {
            directoryPath = configuration.GetSection("DirectoryPath").Get<DirectoryPath>();         
        }
    }
}
