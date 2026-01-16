using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace momoBot.config
{
    internal class jsonReader
    {
        public string token { get; set; }
        public string prefix { get; set; }

        public async Task readJson()
        {
            using (StreamReader sr = new StreamReader("config.json"))
            {
                string json = await sr.ReadToEndAsync();
                jsonStructure data = JsonConvert.DeserializeObject<jsonStructure>(json);

                this.token = data.token;
                this.prefix = data.prefix;
            }
        }
    }

    internal sealed class jsonStructure
    {
        public string token { get; set; }
        public string prefix { get; set; }
    }
}
