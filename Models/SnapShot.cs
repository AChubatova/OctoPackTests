namespace OctoPackTests.Models
{
    public class SnapShot
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public int Version { get; set; }
        public Variable[] Variables { get; set; }
        public ScopeValues ScopeValues { get; set; }
    }
}