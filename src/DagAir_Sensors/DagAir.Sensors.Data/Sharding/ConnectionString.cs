namespace DagAir.Sensors.Data.Sharding
{
    public readonly struct ConnectionString
    {
        public ConnectionString(string name, string value)
        {
            Name = name;
            Value = value;
        }
        
        public string Name { get; }
        public string Value { get; }
    }
}