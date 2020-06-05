using System;
using System.Collections;

public abstract class StraightWalkAlgo: AlgorithmBase
{
    protected StraightWalkAlgo(int width, int height)  : base(width, height)
    {
    }
    
    protected override IEnumerator WalkThroughCellsCoRoutine(Action<int, int> fn)
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                yield return null;
                fn(x, y);
            }
        }
    }
    protected override void EndLastItem(GridGeneratorBase mono, int x, int y)
    {
        if (x * y != (Width - 1) * (Height - 1)) return;
        base.EndLastItem(mono, x, y);
    }

    public abstract override void Execute(GridGeneratorBase mono);
    protected bool IsTopCell(int y)
    {
        return y == Height - 1;
    }
    protected bool IsRightMostCell(int x)
    {
        return x == Width - 1;
    }
}
