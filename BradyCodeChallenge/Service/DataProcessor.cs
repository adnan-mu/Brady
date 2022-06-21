using BradyCodeChallenge.Model;
using Microsoft.Extensions.Configuration;

namespace BradyCodeChallenge.Service
{
    internal class DataProcessor
    {
        private readonly IConfiguration configuration;
        private DirectoryPath directoryPath;        
        private IDataReader dataReader;
        private IDataWriter dataWriter;
        private IEnumerable<WindGenerator> windData;
        private IEnumerable<GasGenerator> gasData;
        private IEnumerable<CoalGenerator> coalData;
        private ReferenceData referenceData;

        public DataProcessor(IConfiguration configuration, IDataReader dataReader, IDataWriter dataWriter)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            ArgumentNullException.ThrowIfNull(dataReader);
            ArgumentNullException.ThrowIfNull(dataWriter);
            this.configuration = configuration;
            this.dataReader = dataReader;
            this.dataWriter = dataWriter;
            ReadConfig();
        }

        public void ProcessData()
        {
            ReadData();
            GenerateReport();
        }

        private void GenerateReport()
        {
            dataWriter.GenerateReport(windData, gasData, coalData, referenceData);
        }

        private void ReadData()
        {
            windData = dataReader.ReadWindData();
            gasData = dataReader.ReadGasData();
            coalData = dataReader.ReadCoalData();
            referenceData = dataReader.ReadReferenceData();
        }

        private void ReadConfig()
        {
            directoryPath = configuration.GetSection("DirectoryPath").Get<DirectoryPath>();

            Console.WriteLine($"Input file is {directoryPath.InputFile}");
            Console.WriteLine($"Output file is {directoryPath.OutputFile}");
        }
    }

   
}
