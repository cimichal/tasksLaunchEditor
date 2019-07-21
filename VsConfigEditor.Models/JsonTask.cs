using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Debugger.Models
{
    public class JsonTask
    {
        private string _defaultArg = "build";
        private string _defaultPrefix = "${workspaceFolder}";
        public string Version { get; set; }
        public List<Task> Tasks { get; set; }
        private Task Task => Tasks.Single();

        public Task SetupConfiguration(Solution solution)
        {
            var task = Task;
            task.Args = new List<string>();

            task.Args.Add(_defaultArg);
            task.Args.Add($"{_defaultPrefix}{solution.Value}");

            return task;
        }
    }

    public class Task
    {
        public List<string> Args { get; set; }
        public string Label { get; set; }
        public string Command { get; set; }
        public string Type { get; set; }
        public string ProblemMatcher { get; set; }
    }
}
