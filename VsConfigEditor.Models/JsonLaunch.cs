using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Models
{
    public class JsonLaunch
    {
        private string DefaultPrefix = "${workspaceRoot}";
        private string BinPath = "/bin/Debug/netcoreapp2.2";
        private string Ext = ".dll";
        public string Version { get; set; }
        public List<LaunchConfigurations> Configurations { get; set; }
        private LaunchConfigurations Configuration => Configurations.Single();
        public LaunchConfigurations SetupConfiguration(Solution solution)
        {
            var configuraton = this.Configuration;
            configuraton.Program = $"{DefaultPrefix}{solution.Value}{BinPath}/{solution.Key}{Ext}";
            configuraton.cwd = $"{DefaultPrefix}{solution.Value}";

            return configuraton;
        }
    }

    public class LaunchConfigurations
    {
        public string Program { get; set; }
        public string cwd { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Request { get; set; }
        public string PreLaunchTask { get; set; }
        public bool StopAtEntry { get; set; }
        public string Console { get; set; }
        public object[] Args { get; set; }
    }
}
