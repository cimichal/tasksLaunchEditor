using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Models
{
    public class JsonLaunch
    {

        public string version { get; set; }
        public List<LaunchConfigurations> configurations { get; set; }

        public LaunchConfigurations SetupConfiguration(Config config, Solution solution)
        {
            var _defaultPrefix = config.prefix;
            var _binPath = config.dllPath;

            var configuraton = configurations.Single();

            configuraton.Program = $"{_defaultPrefix}{solution.Value}{_binPath}/{solution.Key}.dll";
            configuraton.cwd = $"{_defaultPrefix}{solution.Value}";

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
