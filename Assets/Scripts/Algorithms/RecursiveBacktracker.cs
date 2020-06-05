using System;
using System.Collections.Generic;
using StaticClasses;

public class RecursiveBacktracker: HuntAndKill
{
    private readonly List<Tuple<int, int>> _pathList;
    protected override bool ContinueWalkCondition => _pathList.Count > 0;
    public RecursiveBacktracker(int width, int height)  : base(width, height)
    {
        _pathList = new List<Tuple<int, int>>();
    }
    
    protected override Tuple<int, int> SetNewCellPosition()
    {
        int currX = -1;
        int currY = -1;
        for (int i = _pathList.Count-1; i > -1; i --)
        {
            _pathList.RemoveAt(i);
            //Debug.Log(_pathList.Count);
            if (_pathList.Count == 0) break;
            var (x, y) = _pathList[i-1];
            var validDirections = GetValidDirections(x, y);
            if (validDirections.Count <= 0) continue;
            currX = x;
            currY = y;
            TempDirection = Utils.RandomSelectionFromArray(validDirections.ToArray());
            break;
        }
        return new Tuple<int, int>(currX,currY);
    }

    protected override void SetInitialCellPosition(Action<int, int> fn)
    {
        base.SetInitialCellPosition(fn);
        _pathList.Add(new Tuple<int, int>(X,Y));
    }
    protected override bool WillDeleteWall(int x, int y)
    {
        _pathList.Add(new Tuple<int, int>(x, y));
        return true;
    }
    protected override void EndLastItem(GridGeneratorBase mono, int x, int y)
    {
        mono.EndCoroutine();
    }
    public override void Execute(GridGeneratorBase mono)
    {
        mono.StartCoroutine(WalkThroughCellsCoRoutine((x, y) =>
        {
            if (x == -1)
                EndLastItem(mono, x, y);
            else if (!Cells[x][y].IsVisited)
                Cells[x][y].IsVisited = true;
            
        }));
    }
}