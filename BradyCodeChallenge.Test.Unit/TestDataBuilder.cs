using BradyCodeChallenge.Model;

namespace BradyCodeChallenge.Test.Unit
{
    internal class TestDataBuilder
    {
        static public List<CoalGenerator>?  BuildCoalData() {
            return new List<CoalGenerator> {
                new CoalGenerator{
                    Name = "Coal[1]",
                    TotalHeatInput = 11.815m,
                    EmissionsRating = 0.482m,
                    ActualNetGeneration = 11.815m,
                    Generation = new List<Day> {
                        new Day{
                            Date = DateTime.Parse("2017-01-01T00:00:00+00:00"),
                            Energy = 350.487m,
                            Price = 10.146m
                        },
                        new Day{
                            Date = DateTime.Parse("2017-01-02T00:00:00+00:00"),
                            Energy = 348.611m,
                            Price = 11.815m
                        },
                        new Day{
                            Date = DateTime.Parse("2017-01-03T00:00:00+00:00"),
                            Energy = 0m,
                            Price = 11.815m
                        }
                    },
                }
            };
        }

        internal static IEnumerable<GasGenerator>? BuildGasData()
        {
            return new List<GasGenerator> {
                new GasGenerator{                    
                    EmissionsRating = 0.038m,
                    Name = "Gas[1]",
                    Generation = new List<Day> {
                        new Day{
                            Date = DateTime.Parse("2017-01-01T00:00:00+00:00"),
                            Energy = 259.235m,
                            Price = 15.837m
                        },
                        new Day{
                            Date = DateTime.Parse("2017-01-02T00:00:00+00:00"),
                            Energy = 235.975m,
                            Price = 16.556m
                        },
                        new Day{
                            Date = DateTime.Parse("2017-01-03T00:00:00+00:00"),
                            Energy = 240.325m,
                            Price = 17.551m
                        }
                    },
                }
            };
        }

        internal static IEnumerable<WindGenerator>? BuildWindData()
        {
            return new List<WindGenerator> {
                new WindGenerator{
                    Location = "Offshore",
                    Name = "Wind[Offshore]",
                    Generation = new List<Day> {
                        new Day{
                            Date = DateTime.Parse("2017-01-01T00:00:00+00:00"),
                            Energy = 100.368m,
                            Price = 20.148m
                        },
                        new Day{
                            Date = DateTime.Parse("2017-01-02T00:00:00+00:00"),
                            Energy = 90.843m,
                            Price = 25.516m
                        },
                        new Day{
                            Date = DateTime.Parse("2017-01-03T00:00:00+00:00"),
                            Energy = 87.843m,
                            Price = 22.015m
                        }
                    },
                },
                new WindGenerator{
                    Location = "Onshore",
                    Name = "Wind[Onshore]",
                    Generation = new List<Day> {
                        new Day{
                            Date = DateTime.Parse("2017-01-01T00:00:00+00:00"),
                            Energy = 56.578m,
                            Price = 29.542m
                        },
                        new Day{
                            Date = DateTime.Parse("2017-01-02T00:00:00+00:00"),
                            Energy = 48.540m,
                            Price = 22.954m
                        },
                        new Day{
                            Date = DateTime.Parse("2017-01-03T00:00:00+00:00"),
                            Energy = 98.167m,
                            Price = 24.059m
                        }
                    },
                }
            };
        }

        internal static ReferenceData? BuildReferenceData()
        {
            return new ReferenceData { 
                valueFactor = new FactorData { 
                    High = 0.946m,
                    Medium = 0.696m,
                    Low = 0.265m
                },
                emissionFactor = new FactorData {
                    High = 0.812m,
                    Medium = 0.562m,
                    Low = 0.312m
                }
            };
        }
    }
}
