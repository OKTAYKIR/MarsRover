using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using MarsRover.Core.Domain.Enums;
using MarsRover.Core.Domain.Events;
using MarsRover.Core.Domain.ValueTypes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Core.Domain.Aggregates
{
    public class RoverAggregate : AggregateRoot<RoverAggregate, Identity>
    {
        public RoverPosition RoverPosition { get; private set; }
        public Identity PlateauSurfaceId { get; private set; }

        public RoverAggregate(Identity id) : base(id)
        {
        }

        #region Aggregate methods
        public IExecutionResult DeployRover(string roverPositionInput, Identity plateauSurfaceId)
        {
            RoverPosition roverPosition = ParsePosition(roverPositionInput);

            Emit(new DeployRoverEvent(roverPosition, plateauSurfaceId));

            return ExecutionResult.Success();
        }

        public IExecutionResult TurnLeft()
        {
            Emit(new MoveRoverEvent(Movement.L));

            return ExecutionResult.Success();
        }

        public IExecutionResult TurnRight()
        {
            Emit(new MoveRoverEvent(Movement.R));

            return ExecutionResult.Success();
        }

        public async Task<IExecutionResult> MoveAsync()
        {
            if(!await IsRoverInsideBoundariesAsync())
            {
                throw new InvalidOperationException("Rover outside of bounds");
            }

            Emit(new MoveRoverEvent(Movement.M));

            return ExecutionResult.Success();
        }
        #endregion

        #region Apply methods
        public void Apply(DeployRoverEvent aggregateEvent)
        {
           this.RoverPosition = aggregateEvent.RoverPosition;
           this.PlateauSurfaceId = aggregateEvent.PlateauSurfaceId;
        }

        public void Apply(MoveRoverEvent aggregateEvent)
        {
            switch (aggregateEvent.MoveRover)
            {
                case Movement.L:
                    TurnLeftRover();
                    break;

                case Movement.R:
                    TurnRightRover();
                    break;

                case Movement.M:
                    MoveRover();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(aggregateEvent.MoveRover), aggregateEvent.MoveRover, null);
            }
        }
        #endregion

        #region Private methods
        private RoverPosition ParsePosition(string roverPositionInput)
        {
            var roverPositionArray = roverPositionInput.Split(' ');

            if (roverPositionArray.Length == 3)
            {
                string orientation = roverPositionArray[2].ToUpper();

                if (orientation.Equals("N", StringComparison.InvariantCultureIgnoreCase) ||
                    orientation.Equals("S", StringComparison.InvariantCultureIgnoreCase) ||
                    orientation.Equals("E", StringComparison.InvariantCultureIgnoreCase) ||
                    orientation.Equals("W", StringComparison.InvariantCultureIgnoreCase))
                {
                    RoverPosition roverPosition = new RoverPosition() {
                        Orientation = (Orientation)Enum.Parse(typeof(Orientation), orientation),
                        X = int.Parse(roverPositionArray[0]),
                        Y = int.Parse(roverPositionArray[1])
                    };

                    return roverPosition;
                }
            }

            return null;
        }

        private async Task<bool> IsRoverInsideBoundariesAsync()
        {
            IAggregateStore aggregateStore = Helpers.Helpers.RootResolver.Resolve<IAggregateStore>();
            PlateauAggregate plateauAggregate = await aggregateStore.LoadAsync<PlateauAggregate, Identity>(this.PlateauSurfaceId, CancellationToken.None);

            if (RoverPosition.X > plateauAggregate.Size.Width || 
                RoverPosition.X < 0 || 
                RoverPosition.Y > plateauAggregate.Size.Height || 
                RoverPosition.Y < 0)
            {
                return false;
            }

            return true;
        }

        private void TurnRightRover()
        {
            this.RoverPosition.Orientation = (this.RoverPosition.Orientation + 1) > Orientation.W ? Orientation.N : this.RoverPosition.Orientation + 1;
        }

        private void TurnLeftRover()
        {
            this.RoverPosition.Orientation = (this.RoverPosition.Orientation - 1) < Orientation.N ? Orientation.W : this.RoverPosition.Orientation - 1;
        }

        private void MoveRover()
        {
            switch (this.RoverPosition.Orientation)
            {
                case Orientation.N:
                    this.RoverPosition.Y++;
                    break;

                case Orientation.S:
                    this.RoverPosition.Y--;
                    break;
                case Orientation.W:
                    this.RoverPosition.X--;
                    break;

                case Orientation.E:
                    this.RoverPosition.X++;
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
        #endregion
    }
}