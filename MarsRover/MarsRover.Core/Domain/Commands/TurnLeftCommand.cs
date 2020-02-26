using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using MarsRover.Core.Domain.Aggregates;
using MarsRover.Core.Domain.ValueTypes;

namespace MarsRover.Core.Domain.Commands
{
    public class TurnLeftCommand : Command<RoverAggregate, Identity, IExecutionResult>
    {
        public TurnLeftCommand(
            Identity id) 
            : base(id)
        {
        }
    }
}