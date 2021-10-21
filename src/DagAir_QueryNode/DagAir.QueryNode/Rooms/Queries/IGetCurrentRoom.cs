using System.Threading.Tasks;
using DagAir.QueryNode.Rooms.Models;

namespace DagAir.QueryNode.Rooms.Queries
{
    public interface IGetCurrentRoom
    {
        Task<CurrentRoomReadModel> Execute(long id);
    }
}