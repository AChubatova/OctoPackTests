namespace OctoPackTests.Models
{
    public class Variable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Scope Scope { get; set; }
        public bool IsSensitive { get; set; }
        public bool IsEditable { get; set; }
        public object Prompt { get; set; }
    }
}