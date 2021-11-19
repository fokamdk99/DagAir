using System.Threading.Tasks;
using DagAir.Facilities.Rooms.Models;

namespace DagAir.Facilities.Rooms.Queries
{
    public interface IGetCurrentRoom
    {
        Task<CurrentRoomReadModel> Execute(long id);
    }
}