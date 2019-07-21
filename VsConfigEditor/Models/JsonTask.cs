using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Debugger.Models
{
    public class JsonTask
    {
        private string _defaultArg = "build";
        private string _defaultPrefix = "${workspaceFolder}";

        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("tasks")]
        public List<Task> Tasks { get; set; }

        public Task SetupConfiguration(Solution solution)
        {
            var task = Tasks.Single(); ;
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
