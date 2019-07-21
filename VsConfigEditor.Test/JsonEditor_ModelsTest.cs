using NUnit.Framework;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Debugger.Models;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class JsonEditor_ModelsTest
    {
        public string path { get; set; }
        public string _prefix = "C:/Users/michal/Documents/_repository/VsCodeConfigEditor/VsConfigEditor/VsConfigEditor.Test/";
        [SetUp]
        public void Setup()
        {
            path = $"{_prefix}TempFile/config.json";
        }

        [Test]
        public void JsonConfigFile_ShouldBe_JsonExtension()
        {
            string extension = Path.GetExtension(path);
            Assert.AreEqual(".json", extension.ToLower());
        }

        [Test]
        public void JsonConfigFile_ShouldBe_Correct()
        {
            JsonConfig jsonFile;
            using (StreamReader stream = new StreamReader(path))
            {
                string bytes = stream.ReadToEnd();
                jsonFile = JsonConvert.DeserializeObject<JsonConfig>(bytes);
            }

            Assert.IsNotNull(jsonFile.Config);
            Assert.IsNotNull(jsonFile.Config.dllPath);
            Assert.IsNotNull(jsonFile.Config.prefix);
            Assert.IsNotNull(jsonFile.Solutions);
            Assert.IsNotNull(jsonFile.Solutions[0].Active);
        }

        [Test]
        public void JsonLanuchFile_ShouldBeCorrect()
        {
            var path = $"{_prefix}TempFile/launch.json";

            JsonLaunch jsonFile;
            using (StreamReader stream = new StreamReader(path))
            {
                string bytes = stream.ReadToEnd();
                jsonFile = JsonConvert.DeserializeObject<JsonLaunch>(bytes);
            }

            Assert.IsNotNull(jsonFile.Configurations[0].Name);
        }

        [Test]
        public void JsonTasksFile_ShouldBeCorrect()
        {
            var path = $"{_prefix}TempFile/tasks.json";

            JsonTask jsonFile;
            using (StreamReader stream = new StreamReader(path))
            {
                string bytes = stream.ReadToEnd();
                jsonFile = JsonConvert.DeserializeObject<JsonTask>(bytes);
            }

            Assert.IsNotNull(jsonFile.Tasks[0].Args);
        }

        [Test]
        public void JsonConfig__GetActiveSolution__ShouldReturnSingleSolution()
        {
            var moqJsonConfig = new JsonConfig();
            moqJsonConfig.Solutions = new List<Solution>()
                {
                    new Solution() { Active = false },
                    new Solution() { Active = true, Key = "active" }
                };

            Solution active = moqJsonConfig.GetActiveSolution();
            Assert.IsNotNull(active);
            Assert.AreEqual("active", active.Key);
        }

        [Test]
        public void JsonLaunch__SetupConfiguration__ShouldUpdateConfiguration()
        {
            var path = $"{_prefix}TempFile/launch.json";
            JsonLaunch jsonFile;
            using (StreamReader stream = new StreamReader(path))
            {
                string bytes = stream.ReadToEnd();
                jsonFile = JsonConvert.DeserializeObject<JsonLaunch>(bytes);
            }

            var lanuchConfig = jsonFile.SetupConfiguration(new Solution()
            {
                Key = "colections",
                Value = "/przyklady__c#_7/collections/colections"
            });

            Assert.AreEqual("${workspaceRoot}/przyklady__c#_7/collections/colections/bin/Debug/netcoreapp2.2/colections.dll", 
                lanuchConfig.Program);
            Assert.AreEqual("${workspaceRoot}/przyklady__c#_7/collections/colections", lanuchConfig.cwd);
        }

        [Test]
        public void JsonTask__SetupConfiguration__ShouldUpdateConfiguration()
        {
            var path = $"{_prefix}TempFile/tasks.json";
            JsonTask jsonFile;
            using (StreamReader stream = new StreamReader(path))
            {
                string bytes = stream.ReadToEnd();
                jsonFile = JsonConvert.DeserializeObject<JsonTask>(bytes);
            }

            Task lanuchConfig = jsonFile.SetupConfiguration(new Solution()
            {
                Key = "colections",
                Value = "/przyklady__c#_7/collections/colections"
            });

            var contains = lanuchConfig.Args.Contains("${workspaceFolder}/przyklady__c#_7/collections/colections");
            Assert.IsTrue(contains);
        }
    }
}