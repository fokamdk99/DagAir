using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.IngestionNode.Queries
{
    public class InfluxDataController : IngestionNodeController
    {
        [HttpGet]
        public async Task<IActionResult> GetRoomNumber(string sensorId)
        {
            return Ok();
        }
    }
}