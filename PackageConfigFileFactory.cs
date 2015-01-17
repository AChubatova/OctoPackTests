using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Ionic.Zip;
using NUnit.Framework;

namespace OctoPackTests
{
    public class PackageConfigFileFactory
    {
        private static string PkgPath
        {
            get
            {
                return ConfigurationManager.AppSettings["PkgPath"];
            }
        }

        public static IEnumerable ConfigVariables
        {
            get
            {
                if (!System.IO.File.Exists(PkgPath))
                    throw new FileNotFoundException(PkgPath);

                if (ZipFile.IsZipFile(PkgPath))
                {
                    using (var zip = new ZipFile(PkgPath))
                    {
                        var configFiles = zip.Entries.Where(p => p.FileName.EndsWith(".config"));
                        foreach (var configFile in configFiles)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                configFile.Extract(memoryStream);
                                memoryStream.Position = 0;

                                using (var streamReader = new StreamReader(memoryStream))
                                {
                                    var fileData = streamReader.ReadToEnd();

                                    var matches = Regex.Matches(fileData, @"#{(?<VariableName>.+?)}", RegexOptions.Multiline);

                                    if (matches.Count > 0)
                                    {
                                        foreach (var variable in matches
                                            .Cast<Match>()
                                            .Where(p => p.Groups["VariableName"].Success)
                                            .Select(match => match.Groups["VariableName"].Value).Distinct())
                                        {
                                            if (
                                                !variable.StartsWith("if ", StringComparison.InvariantCultureIgnoreCase) &&
                                                !variable.StartsWith("/if", StringComparison.InvariantCultureIgnoreCase) &&
                                                !variable.StartsWith("unless ",
                                                    StringComparison.InvariantCultureIgnoreCase) &&
                                                !variable.StartsWith("/unless",
                                                    StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                yield return new TestCaseData(configFile.FileName, variable);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static IEnumerable ConfigFiles
        {
            get
            {
                if (!System.IO.File.Exists(PkgPath))
                    throw new FileNotFoundException(PkgPath);

                if (ZipFile.IsZipFile(PkgPath))
                {
                    using (var zip = new ZipFile(PkgPath))
                    {
                        var configFiles = zip.Entries.Where(p => p.FileName.EndsWith(".config"));
                        foreach (var configFile in configFiles)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                configFile.Extract(memoryStream);
                                memoryStream.Position = 0;

                                using (var streamReader = new StreamReader(memoryStream))
                                {
                                    var fileData = streamReader.ReadToEnd();

                                    yield return new TestCaseData(configFile.FileName, fileData)
                                        .SetName(configFile.FileName);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}