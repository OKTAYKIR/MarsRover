using EventFlow.Aggregates;
using EventFlow.EventStores;
using MarsRover.Core.Domain.Aggregates;
using MarsRover.Core.Domain.Enums;
using MarsRover.Core.Domain.ValueTypes;

namespace MarsRover.Core.Domain.Events
{
    [EventVersion("MoveRover", 1)]
    public class MoveRoverEvent : AggregateEvent<RoverAggregate, Identity>
    {
        public MoveRoverEvent(Movement movement)
        {
            MoveRover = movement;
        }

        public Movement MoveRover { get; set; }
    }
}