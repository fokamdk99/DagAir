using System;

namespace DagAir.AdminNode.Contracts.Commands
{
    public class GetRoomCommand : ICommand
    {
        public long RoomId { get; set; }
        public int NumberOfRecords { get; set; }
    }
}