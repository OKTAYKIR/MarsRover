using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using MarsRover.Core.Domain.Aggregates;
using MarsRover.Core.Domain.Commands;
using MarsRover.Core.Domain.ValueTypes;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Application.CommandHandlers
{
    public class TurnRightCommandHandler :
         CommandHandler<RoverAggregate, Identity, IExecutionResult, TurnRightCommand>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync(
            RoverAggregate aggregate,
            TurnRightCommand command,
            CancellationToken cancellationToken)
        {
            IExecutionResult executionResult = aggregate.TurnRight();

            return await Task.FromResult(executionResult);
        }
    }
}