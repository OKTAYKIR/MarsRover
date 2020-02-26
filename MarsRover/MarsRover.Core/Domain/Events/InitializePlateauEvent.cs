using EventFlow.Aggregates;
using EventFlow.EventStores;
using MarsRover.Core.Domain.Aggregates;
using MarsRover.Core.Domain.ValueTypes;

namespace MarsRover.Core.Domain.Events
{
    [EventVersion("InitializePlateau", 1)]
    public class InitializePlateauEvent : AggregateEvent<PlateauAggregate, Identity>
    {
        public InitializePlateauEvent(SurfaceSize surfaceSize)
        {
            Size = surfaceSize;
        }

        public SurfaceSize Size { get; set; }
    }
}