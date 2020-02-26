using MarsRover.Core.Domain.Enums;

namespace MarsRover.Core.Domain.ValueTypes
{
    public class RoverPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Orientation Orientation { get; set; }

        public RoverPosition(
            Orientation orientation = Orientation.N, 
            int x = 0, 
            int y = 0)
        {
            this.X = x;
            this.Y = y;
            this.Orientation = orientation;
        }
    }
}
