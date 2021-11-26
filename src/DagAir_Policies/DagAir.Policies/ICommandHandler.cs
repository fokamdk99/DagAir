using System.Threading.Tasks;
using DagAir.Policies.Contracts.Commands;

namespace DagAir.Policies
{
    public interface ICommandHandler<TCommand, TReturnObject> 
        where TCommand : ICommand 
        where TReturnObject : class
    {
        Task<TReturnObject> Handle(TCommand command);
    }
}