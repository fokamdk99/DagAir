using System;

namespace DagAir.IngestionNode.Contracts
{
    public class UserIdentity
    {
        public UserIdentity(long userId, string identifier)
        {
            Id = userId;
            Identifier = identifier;
        }

        public UserIdentity(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            Id = user.Id;
            Identifier = user.Identifier;
        }
        
        public long Id { get; init; }
        public string Identifier { get; init; }
    }
}