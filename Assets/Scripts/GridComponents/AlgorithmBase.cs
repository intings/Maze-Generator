using System;
using System.Collections;
public abstract class AlgorithmBase: GridBase
{

    /// <summary>
    /// RecursiveBacktracker does not call this
    /// it completely overrides something it doesn't want
    /// from RandomWalkAlgo
    /// </summary>
    protected virtual void EndLastItem(GridGeneratorBase mono, int x, int y)
    {
        mono.EndCoroutine();
    }

    public abstract void Execute(GridGeneratorBase gridGeneratorBase);

    protected AlgorithmBase(int width, int height) : base(width, height)
    {
    }
    
    protected abstract IEnumerator WalkThroughCellsCoRoutine(Action<int, int> fn);
    
}
