using System.Threading.Tasks;

namespace DagAir.Facilities.Rooms.Commands
{
    public interface IDeleteRoomHandler
    {
        Task<int> Handle(long roomId);
    }
}