using Debugger.Logic;
using Debugger.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestFixture]
    public class JsonEditorTest
    {
        IDictionary<FileType, string> filesPath;
        [SetUp]
        public void Setup()
        {
            filesPath = new Dictionary<FileType, string>()
            {
                { FileType.Config, "C:/Users/michal/Documents/_repository/VsCodeConfigEditor/VsConfigEditor/VsConfigEditor.Test/TempFile/config.json" },
                { FileType.Launch, "launch.json" },
                { FileType.Task, "tasks.json" }
            };
        }

        [Test]
        public void ShouldReturn_ActiveConfiguration()
        {
            var jsonEditor = new JsonEditor(filesPath);
            Solution activeSolution = jsonEditor.GetActiveConfiguration();

            Assert.AreEqual("serializacja", activeSolution.Key);
        }
    }
}
