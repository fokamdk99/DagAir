using System.Threading.Tasks;
using DagAir.Facilities.Contracts.Commands;

namespace DagAir.Facilities
{
    public interface ICommandHandler<TCommand, TReturnObject> 
        where TCommand : ICommand 
        where TReturnObject : class
    {
        Task<TReturnObject> Handle(TCommand command);
    }
}