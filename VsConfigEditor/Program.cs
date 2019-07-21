using Debugger.Logic;
using Debugger.Models;
using System;
using System.Collections.Generic;

namespace Debugger.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentNullException("root path is expexted");

            var rootPath = args[0];
            IDictionary<FileType, string> filesPath = new Dictionary<FileType, string>()
            {
                { FileType.Config, $"{rootPath}config.json" },
                { FileType.Launch, $"{rootPath}launch.json" },
                { FileType.Task,   $"{rootPath}tasks.json" }
            };
            JsonEditor jsonEditor = new JsonEditor(filesPath);

            var config = jsonEditor.GetActiveConfiguration();
            var jsonLanuch = jsonEditor.LoadLunchJson();
            var jsonTask = jsonEditor.LoadTaskJson();

            jsonLanuch.SetupConfiguration(config);
            jsonTask.SetupConfiguration(config);

            jsonEditor.Update<JsonLaunch>(jsonLanuch);
            jsonEditor.Update<JsonTask>(jsonTask);
        }
    }
}
