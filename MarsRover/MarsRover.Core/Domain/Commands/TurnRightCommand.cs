using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using MarsRover.Core.Domain.Aggregates;
using MarsRover.Core.Domain.ValueTypes;

namespace MarsRover.Core.Domain.Commands
{
    public class TurnRightCommand : Command<RoverAggregate, Identity, IExecutionResult>
    {
        public TurnRightCommand(
            Identity id) 
            : base(id)
        {
        }
    }
}