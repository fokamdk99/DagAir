using System;

namespace DagAir.Sensors.Contracts.DTOs
{
    public class SensorDto
    {
        public long Id { get; set; }
        public string SensorName { get; set; }
        public DateTime LastDataSentDate { get; set; }
        public long SensorModelId { get; set; }
        public long RoomId { get; set; }
        public long AffiliateId { get; set; }
        public SensorModelDto SensorModel { get; set; }
    }
}