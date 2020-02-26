using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using MarsRover.Core.Domain.Aggregates;
using MarsRover.Core.Domain.Commands;
using MarsRover.Core.Domain.ValueTypes;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Application.CommandHandlers
{
    public class CreatePlateauSurfaceCommandHandler :
         CommandHandler<PlateauAggregate, Identity, IExecutionResult, CreatePlateauSurfaceCommand>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync(
            PlateauAggregate aggregate,
            CreatePlateauSurfaceCommand command,
            CancellationToken cancellationToken)
        {
            IExecutionResult executionResult = aggregate.Initialize(command.SurfaceSizeInput);

            return await Task.FromResult(executionResult);
        }
    }
}