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
        public string _prefix;
        [SetUp]
        public void Setup()
        {
            _prefix = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.AppDomain.CurrentDomain.RelativeSearchPath ?? "");
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

            Assert.IsNotNull(jsonFile.configurations[0].Name);
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

            Assert.IsNotNull(jsonFile.tasks[0].Args);
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
            var lanuchConfig = jsonFile.SetupConfiguration(new Config()
            {
                active = "colections",
                dllPath = "/bin/Debug/netcoreapp2.2",
                prefix = "${workspaceFolder}"
            }, new Solution()
            {
                Key = "colections",
                Value = "/przyklady__c#_7/collections/colections"
            });

            Assert.AreEqual("${workspaceFolder}/przyklady__c#_7/collections/colections/bin/Debug/netcoreapp2.2/colections.dll",
                lanuchConfig.Program);
            Assert.AreEqual("${workspaceFolder}/przyklady__c#_7/collections/colections", lanuchConfig.cwd);
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

            Task lanuchConfig = jsonFile.SetupConfiguration(new Config()
            {
                active = "colections",
                dllPath = "/bin/Debug/netcoreapp2.2",
                prefix = "${workspaceFolder}"
            }, new Solution()
            {
                Key = "colections",
                Value = "/przyklady__c#_7/collections/colections"
            });

            var contains = lanuchConfig.Args.Contains("${workspaceFolder}/przyklady__c#_7/collections/colections");
            Assert.IsTrue(contains);
        }
    }
}