using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using MarsRover.Core.Domain.Aggregates;
using MarsRover.Core.Domain.Commands;
using MarsRover.Core.Domain.ValueTypes;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Application.CommandHandlers
{
    public class TurnLeftCommandHandler :
         CommandHandler<RoverAggregate, Identity, IExecutionResult, TurnLeftCommand>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync(
            RoverAggregate aggregate,
            TurnLeftCommand command,
            CancellationToken cancellationToken)
        {
            IExecutionResult executionResult = aggregate.TurnLeft();

            return await Task.FromResult(executionResult);
        }
    }
}