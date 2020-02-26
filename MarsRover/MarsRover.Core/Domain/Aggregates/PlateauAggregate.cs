using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using MarsRover.Core.Domain.Events;
using MarsRover.Core.Domain.ValueTypes;

namespace MarsRover.Core.Domain.Aggregates
{
    public class PlateauAggregate : AggregateRoot<PlateauAggregate, Identity>
    {
        public PlateauAggregate(Identity id) : base(id)
        {
        }

        public SurfaceSize Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        #region Aggregate methods
        public IExecutionResult Initialize(string surfaceSizeInput)
        {
            SurfaceSize surfaceSize = null;

            if (!string.IsNullOrWhiteSpace(surfaceSizeInput))
            {
                var gridSize = surfaceSizeInput.Split(' ');

                if (gridSize.Length == 2)
                {
                    if (int.TryParse(gridSize[0], out int width))
                    {
                        if (int.TryParse(gridSize[1], out int height))
                        {
                            surfaceSize = new SurfaceSize(width, height);
                        }
                    }
                }
            }

            if (surfaceSize == null)
                return null;

            Emit(new InitializePlateauEvent(surfaceSize));

            return ExecutionResult.Success();
        }
        #endregion

        #region Apply methods
        public void Apply(InitializePlateauEvent aggregateEvent)
        {
            Size = aggregateEvent.Size;
        }
        #endregion
    }
}