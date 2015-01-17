namespace OctoPackTests.Models
{
    public class Releases
    {
        public string ItemType { get; set; }
        public bool IsStale { get; set; }
        public int TotalResults { get; set; }
        public int ItemsPerPage { get; set; }
        public Release[] Items { get; set; }
    }
}
