using System.Threading.Tasks;
using DagAir.Addresses.Contracts.Commands;

namespace DagAir.Addresses
{
    public interface ICommandHandler<TCommand, TReturnObject> 
        where TCommand : ICommand 
        where TReturnObject : class
    {
        Task<TReturnObject> Handle(TCommand command);
    }
}