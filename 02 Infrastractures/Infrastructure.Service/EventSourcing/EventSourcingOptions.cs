namespace YQB.Infra.Service.EventSourcing
{
    public class EventSourcingOptions
    {
        public bool IsEnable { get; set; }
        public string ConnectionString { get; set; }
        public string ApplicationName { get; set; }
    }
}