using System.Threading.Tasks;
using DagAir.Facilities.Data.AppEntitities;

namespace DagAir.Facilities.Rooms.Queries
{
    public interface IGetCurrentRoomQuery
    {
        Task<Room> Execute(long id);
    }
}