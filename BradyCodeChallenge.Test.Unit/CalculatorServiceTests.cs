using BradyCodeChallenge.Constant;
using BradyCodeChallenge.Model;
using FluentAssertions;
using System.Xml.Linq;

namespace BradyCodeChallenge.Test.Unit
{
    
    public class CalculatorServiceTests
    {
        private readonly IEnumerable<CoalGenerator> coalData;
        private readonly ReferenceData referenceData;
        private readonly IEnumerable<WindGenerator> windData;
        private readonly IEnumerable<GasGenerator> gasData;        

        public CalculatorServiceTests()
        {
            windData = TestDataBuilder.BuildWindData();
            gasData = TestDataBuilder.BuildGasData();
            coalData = TestDataBuilder.BuildCoalData();
            referenceData = TestDataBuilder.BuildReferenceData(); ;
        }

        [Fact]
        public void CalculateCoalTotalReturnsValidElement()
        {
            // Arrange
            var expectedResult = new XElement(DataConstant.Generator,
                                new XElement(DataConstant.Name, "Coal[1]"),
                                new XElement(DataConstant.Total, 5341.716526632m));                               

            //Act 
            var sut = new CalculatorService();
            var result = sut.CalculateCoalTotal(coalData, referenceData);

            //Assert
            result.FirstOrDefault().ToString().Should().Be(expectedResult.ToString());
        }

        [Fact]
        public void CalculateGasTotalReturnsValidElement()
        {
            // Arrange
            var expectedResult = new XElement(DataConstant.Generator,
                                new XElement(DataConstant.Name, "Gas[1]"),
                                new XElement(DataConstant.Total, 8512.254605520m));

            //Act 
            var sut = new CalculatorService();
            var result = sut.CalculateGasTotal(gasData, referenceData);

            //Assert
            result.FirstOrDefault().ToString().Should().Be(expectedResult.ToString());
        }

        [Fact]
        public void CalculateWindTotalReturnsValidElement()
        {
            // Arrange
            var expectedFistResult = new XElement(DataConstant.Generator,
                                new XElement(DataConstant.Name, "Wind[Offshore]"),
                                new XElement(DataConstant.Total, 1662.617445705m));
            var expectedLastResult = new XElement(DataConstant.Generator,
                                new XElement(DataConstant.Name, "Wind[Onshore]"),
                                new XElement(DataConstant.Total, 4869.453917394m));

            //Act 
            var sut = new CalculatorService();
            var result = sut.CalculateWindTotal(windData, referenceData);

            //Assert
            result.FirstOrDefault().ToString().Should().Be(expectedFistResult.ToString());
            result.LastOrDefault().ToString().Should().Be(expectedLastResult.ToString());
        }

        [Fact]
        public void CalculateActualHeatRateReturnsValidElement()
        {
            // Arrange
            var expectedResult = new XElement(DataConstant.ActualHeatRate,
                                new XElement(DataConstant.Name, "Coal[1]"),
                                new XElement(DataConstant.HeatRate, 1m));

            //Act 
            var sut = new CalculatorService();
            var result = sut.CalculateActualHeatRate(coalData);

            //Assert
            result.FirstOrDefault().ToString().Should().Be(expectedResult.ToString());
        }

        [Fact]
        public void CalculateHighestDailyEmissionReturnsValidElement()
        {
            // Arrange
            var expectedFirstResult = new XElement(DataConstant.Day,
                                new XElement(DataConstant.Name, "Coal[1]"),
                                new XElement(DataConstant.Date, "2017-01-01T00:00:00+00:00"),
                                new XElement(DataConstant.Emission, 137.175004008m));

            var expectedSecondResult = new XElement(DataConstant.Day,
                               new XElement(DataConstant.Name, "Coal[1]"),
                               new XElement(DataConstant.Date, "2017-01-02T00:00:00+00:00"),
                               new XElement(DataConstant.Emission, 136.440767624m));

            var expectedThirdResult = new XElement(DataConstant.Day,
                               new XElement(DataConstant.Name, "Gas[1]"),
                               new XElement(DataConstant.Date, "2017-01-03T00:00:00+00:00"),
                               new XElement(DataConstant.Emission, 5.132380700m));

            //Act 
            var sut = new CalculatorService();
            var result = sut.CalculateHighestDailyEmission(gasData, coalData, referenceData);

            //Assert
            result.FirstOrDefault().ToString().Should().Be(expectedFirstResult.ToString());
            result.Skip(1).FirstOrDefault().ToString().Should().Be(expectedSecondResult.ToString());
            result.Skip(2).FirstOrDefault().ToString().Should().Be(expectedThirdResult.ToString());
        }
    }
}