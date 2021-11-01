using System.Collections.Generic;

namespace DagAir.Sensors.Contracts.DTOs
{
    public class SensorModelDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public long ProducerId { get; set; }
        public ProducerDto Producer { get; set; }
        public ICollection<SensorDto> Sensors { get; set; }
    }
}