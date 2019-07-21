using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Models
{
    public class JsonLaunch
    {
        private string DefaultPrefix = "${workspaceFolder}";
        private string BinPath = "/bin/Debug/netcoreapp2.2";
        private string Ext = ".dll";
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("configurations")]
        public List<LaunchConfigurations> Configurations { get; set; }

        public LaunchConfigurations SetupConfiguration(Solution solution)
        {
            var configuraton = Configurations.Single();
            configuraton.Program = $"{DefaultPrefix}{solution.Value}{BinPath}/{solution.Key}{Ext}";
            configuraton.cwd = $"{DefaultPrefix}{solution.Value}";

            return configuraton;
        }
    }

    public class LaunchConfigurations
    {
        [JsonProperty("program")]
        public string Program { get; set; }

        [JsonProperty("cwd")]
        public string cwd { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("request")]
        public string Request { get; set; }

        [JsonProperty("preLaunchTask")]
        public string PreLaunchTask { get; set; }

        [JsonProperty("stopAtEntry")]
        public bool StopAtEntry { get; set; }

        [JsonProperty("console")]
        public string Console { get; set; }

        [JsonProperty("args")]
        public object[] Args { get; set; }
    }
}
