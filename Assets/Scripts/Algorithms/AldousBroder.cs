using System.Collections.Generic;

namespace Algorithms
{
    public class AldousBroder : RandomWalkAlgo
    {
        public AldousBroder(int width, int height) : base(width, height)
        {
        }

        protected override List<int> GetValidDirections(int x, int y)
        {
            return Cells[x][y].ValidDirections();
        }

        protected override bool WillDeleteWall(int x, int y)
        {
            return (!Cells[x][y].IsVisited);
        }

    }
}