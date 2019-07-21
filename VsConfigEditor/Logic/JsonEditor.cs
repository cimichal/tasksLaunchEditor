using Debugger.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;

namespace Debugger.Logic
{
    public class JsonEditor
    {
        private IDictionary<FileType, string> _filePath;
        private JsonLaunch JsonLaunch;
        private JsonTask JsonTask;

        public JsonEditor(IDictionary<FileType, string> filePath)
        {
            if (!filePath.ContainsKey(FileType.Config))
                throw new InvalidOperationException($"Config dictionary don't contains: {nameof(FileType.Config)}");

            if (!filePath.ContainsKey(FileType.Launch))
                throw new InvalidOperationException($"Config dictionary don't contains: {nameof(FileType.Launch)}");

            if (!filePath.ContainsKey(FileType.Task))
                throw new InvalidOperationException($"Config dictionary don't contains: {nameof(FileType.Task)}");

            _filePath = filePath;
        }

        public Config GetConfig()
        {
            JsonConfig jsonFile;
            using (StreamReader stream = new StreamReader(_filePath[FileType.Config]))
            {
                string bytes = stream.ReadToEnd();
                jsonFile = JsonConvert.DeserializeObject<JsonConfig>(bytes);
            }

            return jsonFile.Config;
        }
        public Solution GetActiveConfiguration()
        {
            JsonConfig jsonFile; 
            using (StreamReader stream = new StreamReader(_filePath[FileType.Config]))
            {
                string bytes = stream.ReadToEnd();
                jsonFile = JsonConvert.DeserializeObject<JsonConfig>(bytes);
            }

            return jsonFile.GetActiveSolution();
        }
        public JsonLaunch LoadLunchJson()
        {
            JsonLaunch jsonFile;
            using (StreamReader stream = new StreamReader(_filePath[FileType.Launch]))
            {
                string bytes = stream.ReadToEnd();
                jsonFile = JsonConvert.DeserializeObject<JsonLaunch>(bytes);
            }
            return jsonFile;
        }

        public JsonTask LoadTaskJson()
        {
            JsonTask jsonFile;
            using (StreamReader stream = new StreamReader(_filePath[FileType.Task]))
            {
                string bytes = stream.ReadToEnd();
                jsonFile = JsonConvert.DeserializeObject<JsonTask>(bytes);
            }
            return jsonFile;
        }

        public void Update<TType>(dynamic file)
        {
            Type type = typeof(TType);

            if (type == typeof(JsonLaunch))
            {
                JsonLaunch _jsonLaunch;
                var _launchPath = _filePath[FileType.Launch];
                using (StreamReader stream = new StreamReader(_launchPath))
                {
                    string bytes = stream.ReadToEnd();
                    _jsonLaunch = JsonConvert.DeserializeObject<JsonLaunch>(bytes);
                }
                
                _jsonLaunch.configurations = (file as JsonLaunch).configurations;
                string output = JsonConvert.SerializeObject(_jsonLaunch, Formatting.Indented);
                File.WriteAllText(_launchPath, output);
            }

            if (type == typeof(JsonTask))
            {
                JsonTask _jsonTask;
                var _taskPath = _filePath[FileType.Task];
                using (StreamReader stream = new StreamReader(_taskPath))
                {
                    string bytes = stream.ReadToEnd();
                    _jsonTask = JsonConvert.DeserializeObject<JsonTask>(bytes);
                }

                _jsonTask.tasks = (file as JsonTask).tasks;
                string output = JsonConvert.SerializeObject(_jsonTask, Formatting.Indented);
                File.WriteAllText(_taskPath, output);
            }
        }
    }
}
