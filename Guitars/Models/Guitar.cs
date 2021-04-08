using Newtonsoft.Json;

namespace Guitars.Models
{
    public class Guitar
    {
        [JsonProperty("make"), JsonRequired]
        public string Make { get; set; }

        [JsonProperty("model"), JsonRequired]
        public string Model { get; set; }

        [JsonProperty("shape"), JsonRequired]
        public string Shape { get; set; }

        [JsonProperty("strings"), JsonRequired]
        public int Strings { get; set; }

        public Guitar(string make, string model, string shape, int strings)
        {
            Make = make;
            Model = model;
            Shape = shape;
            Strings = strings;
        }
    }
}
