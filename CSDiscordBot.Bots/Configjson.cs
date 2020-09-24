using Newtonsoft.Json;

namespace CSDiscordBot
{
    public struct Configjson
    {
        [JsonProperty("token")]
        public string token { get; private set; }
        [JsonProperty("prefix")]
        public string prefix { get; private set; }

    }
}
