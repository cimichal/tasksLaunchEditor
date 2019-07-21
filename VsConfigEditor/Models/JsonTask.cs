using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Debugger.Models
{
    public class JsonTask
    {
        private readonly string _defaultArg = "build";

        public string version { get; set; }
        public List<Task> tasks { get; set; }

        public Task SetupConfiguration(Config config, Solution solution)
        {
            var _defaultPrefix = config.prefix;

            var task = tasks.Single();
            task.Args = new List<string>();

            task.Args.Add(_defaultArg);
            task.Args.Add($"{_defaultPrefix}{solution.Value}");

            return task;
        }
    }

    public class Task
    {
        [JsonProperty("args")]
        public List<string> Args { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("problemMatcher")]
        public string ProblemMatcher { get; set; }
    }
}
