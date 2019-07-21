using System.Collections.Generic;
using System.Linq;

namespace Debugger.Models
{
    public class JsonConfig
    {
        public Config Config { get; set; }
        public IList<Solution> Solutions { get; set; } = new List<Solution>();

        public Solution GetActiveSolution() => Solutions.Single(x => x.Active == true);
    }
    public class Config
    {
        public string prefix { get; set; }
        public string dllPath { get; set; }
    }

    public class Solution
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Active { get; set; }
    }
}
