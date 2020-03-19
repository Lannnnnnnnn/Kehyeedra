using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace Kehyeedra3
{
    class Configuration
    {
        [JsonIgnore]
        public static readonly string appdir = Environment.CurrentDirectory;
        public string Prefix { get; set; }
        public string Token { get; set; }
        public int Shards { get; set; }
        public ulong[] BigBoys { get; set; }
        public string DatabaseHost { get; set; }
        public ushort DatabasePort { get; set; }
        public string DatabaseUser { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseDb { get; set; }

        public Dictionary<string, string> TriggerPhrases { get; set; }

        public Configuration()
        {
            Prefix = "";
            Token = "";
            Shards = 0;
            BigBoys = new ulong[] { 0 };
            DatabaseHost = "127.0.0.1";
            DatabasePort = 3306;
            DatabaseUser = "root";
            DatabasePassword = "";
            DatabaseDb = "yeedra";
            TriggerPhrases = new Dictionary<string, string>();
        }

        public void Save(string dir = "storage/configuration.json")
        {
            string file = Path.Combine(appdir, dir);
            File.WriteAllText(file, ToJson());
        }
        public static Configuration Load(string dir = "storage/configuration.json")
        {
            Bot.EnsureConfigExists();
            string file = Path.Combine(appdir, dir);
            return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(file));
        }

        public string ToJson()
            => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
