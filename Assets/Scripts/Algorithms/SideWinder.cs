using System.Collections.Generic;
using StaticClasses;

namespace Algorithms
{
    public class SideWinder : StraightWalkAlgo
    {
        public SideWinder(int width, int height) : base(width, height)
        {
        }

        public override void Execute(GridGeneratorBase mono)
        {
            int currentY = 0;
            var sideWinderGroup = new List<int>();
            mono.StartCoroutine(WalkThroughCellsCoRoutine((x, y) =>
            {
                int wallToDelete;
                if (IsTopCell(y))
                    wallToDelete = Constants.RIGHT;
                else
                {
                    if (y == currentY)
                        sideWinderGroup.Add(x);
                    else
                    {
                        currentY = y;
                        sideWinderGroup.Clear();
                        sideWinderGroup.Add(x);
                    }
                                
                    bool goUp = IsRightMostCell(x) || Utils.Percentage(50);
                    if (goUp)
                    {
                        x = Utils.RandomSelectionFromArray(sideWinderGroup.ToArray());
                        wallToDelete = Constants.TOP;
                        sideWinderGroup.Clear();
                    }                
                    else wallToDelete = Constants.RIGHT;

                }
                Cells[x][y].DeleteWallWithPosition(wallToDelete);
                EndLastItem(mono, x, y);
            }));
        }
    }
}