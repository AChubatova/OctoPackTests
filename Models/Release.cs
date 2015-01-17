using System;

namespace OctoPackTests.Models
{
    public class Release
    {
        public string Id { get; set; }
        public DateTime Assembled { get; set; }
        public string ReleaseNotes { get; set; }
        public string ProjectId { get; set; }
        public string ProjectVariableSetSnapshotId { get; set; }
        public string[] LibraryVariableSetSnapshotIds { get; set; }
        public string ProjectDeploymentProcessSnapshotId { get; set; }
        public SelectedPackage[] SelectedPackages { get; set; }
        public string Version { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
    }
}