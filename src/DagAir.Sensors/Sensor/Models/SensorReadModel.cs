using System;
using DagAir.QueryNode.Data.AppEntitities;

namespace DagAir.Sensors.Sensor.Models
{
    public record SensorReadModel
    {
        public long Id { get; set; }
        public DateTime LastDataSentDate { get; set; }
        public long RoomId { get; set; }
        public long AffiliateId { get; set; }
        public SensorModel Model { get; set; }

        public SensorReadModel(long id, DateTime lastDataSentDate, long roomId, long affiliateId,
            SensorModel model)
        {
            Id = id;
            LastDataSentDate = lastDataSentDate;
            RoomId = roomId;
            AffiliateId = affiliateId;
            Model = model;
        }
    }
}