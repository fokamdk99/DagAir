using System.Threading.Tasks;

namespace DagAir.Sensors.Sensor.Queries
{
    public interface IQuery<T>
    {
        Task<T> Execute(long Id);
    }
}