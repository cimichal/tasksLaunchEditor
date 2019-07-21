using System;
using System.Collections.Generic;
using System.Linq;

namespace Debugger.Models
{
    public class JsonConfig
    {
        public Config Config { get; set; }
        public IList<Solution> Solutions { get; set; } = new List<Solution>();

        public Solution GetActiveSolution()
        {
            if (string.IsNullOrWhiteSpace(Config.active))
                throw new InvalidOperationException("Active configuration is not set");

            return Solutions.Single(x => x.Key.ToLower() == Config.active.ToLower());
        } 
    }
    public class Config
    {
        public string prefix { get; set; }
        public string dllPath { get; set; }
        public string active { get; set; }
    }

    public class Solution
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Active { get; set; }
    }
}
