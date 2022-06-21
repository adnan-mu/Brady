
namespace BradyCodeChallenge.Model
{
    public abstract class EnergyGenerator
    {
        public string Name {get; set;}        
        public List<Day> Generation { get; set; }
    }
}
