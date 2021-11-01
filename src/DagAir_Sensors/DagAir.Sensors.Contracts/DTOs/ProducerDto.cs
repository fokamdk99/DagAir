using System;
using System.Collections.Generic;

namespace DagAir.Sensors.Contracts.DTOs
{
    public class ProducerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfEstablishment { get; set; }
        public long AddressId { get; set; }
        public virtual ICollection<SensorModelDto> SensorModels { get; set; }
    }
}