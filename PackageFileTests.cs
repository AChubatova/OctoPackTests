using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using OctoPackTests.Models;
using Should;

namespace OctoPackTests
{
    [TestFixture]
    public class PackageFileTests
    {
        private SnapShot snapshotVariables;

        [TestFixtureSetUp]
        public void LoadSnapShotData()
        {
            var snapshotPath = ConfigurationManager.AppSettings["SnapshotPath"];

            if (!System.IO.File.Exists(snapshotPath))
                throw new FileNotFoundException(snapshotPath);

            snapshotVariables = JsonConvert.DeserializeObject<SnapShot>(System.IO.File.ReadAllText(snapshotPath));
        }

        [Test, TestCaseSource(typeof(PackageConfigFileFactory), "ConfigFiles")]
        public void ConfigFilesShouldExist(string fileName, string fileData)
        {
            fileName.ShouldNotBeNull();
        }

        [Test, TestCaseSource(typeof(PackageConfigFileFactory), "ConfigFiles")]
        public void ConfigFilesShouldContainData(string fileName, string fileData)
        {
            fileData.ShouldNotBeNull();
        }

        [Test, TestCaseSource(typeof(PackageConfigFileFactory), "ConfigVariables")]
        public void VariableNamesShouldNotBeNull(string fileName, string variable)
        {
            variable.ShouldNotBeNull();
            variable.ShouldNotBeEmpty();
        }

        [Test, TestCaseSource(typeof(PackageConfigFileFactory), "ConfigVariables")]
        public void VariablesShouldBeIncludedInSnapShot(string fileName, string variable)
        {
            snapshotVariables
                .Variables.Any(p => p.Name.Equals(variable, StringComparison.InvariantCultureIgnoreCase))
                .ShouldBeTrue();
        }
    }
}
