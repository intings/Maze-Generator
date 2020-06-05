using StaticClasses;

namespace Algorithms
{
    public class BinaryTree : StraightWalkAlgo
    {
        public BinaryTree(int width, int height) : base(width, height)
        {
        }

        public override void Execute(GridGeneratorBase mono)
        {
            mono.StartCoroutine(WalkThroughCellsCoRoutine((x, y) =>
            {
                int wallToDelete;
                if (IsTopCell(y))
                    wallToDelete = Constants.RIGHT;
                else if (IsRightMostCell(x))
                    wallToDelete = Constants.TOP;
                else
                    wallToDelete = Utils.Percentage(50) ? Constants.TOP : Constants.RIGHT;
                Cells[x][y].DeleteWallWithPosition(wallToDelete);
                EndLastItem(mono, x, y);
            }));
        }
    }
}
