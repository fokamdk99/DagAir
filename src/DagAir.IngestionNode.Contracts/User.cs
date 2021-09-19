namespace DagAir.IngestionNode.Contracts
{
    public record User
    {
        public long Id { get; init; }
        public string Identifier { get; init; }

        public User(long id, string identifier)
        {
            Id = id;
            Identifier = identifier;
        }
    }
}