using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Algorithms;
using StaticClasses;
using UnityEngine;

public class HuntAndKill: RandomWalkAlgo
{
    protected int TempDirection;
    public HuntAndKill(int width, int height)  : base(width, height)
    {
    }
    
    protected override IEnumerator WalkThroughCellsCoRoutine(Action<int, int> fn)
    {
        SetInitialCellPosition(fn);
        while (ContinueWalkCondition)
        {
            var directions = GetValidDirections(X, Y);
            if (directions.Count > 0)
            {
                int direction = Utils.RandomSelectionFromArray(directions.ToArray());
                (NewX, NewY) = GetNextCellUsingDirection(X, Y, direction);
                if (WillDeleteWall(NewX, NewY))
                    yield return DeleteWall(X, Y, direction);
                (X, Y) = (NewX, NewY);
                fn(X, Y);
            }
            else
            {
                (NewX, NewY) = SetNewCellPosition();
                if (NewX > -1)
                    yield return DeleteWall(NewX, NewY, TempDirection);
                (X, Y) = (NewX, NewY);
                fn(X, Y);
            }
        }
    }

    protected override List<int> GetValidDirections(int x, int y)
    {
        return VisitedCells(x, y, remove: true);
    }

    protected override bool WillDeleteWall(int x, int y)
    {
        return true;
    }

    protected virtual Tuple<int, int> SetNewCellPosition()
    {
        int currX = -1;
        int currY = -1;
        var isBreak = false;
        for (int y = Height - 1; y > -1; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                var c = Cells[x][y];
                if (c.IsVisited) continue;
                var visitedCells = VisitedCells(x, y);
                if (visitedCells.Count <= 0) continue;
                currX = x;
                currY = y;
                isBreak = true;
                TempDirection = Utils.RandomSelectionFromArray(visitedCells.ToArray());
                break;
            }

            if (isBreak) break;
        }
        
        return new Tuple<int, int>(currX,currY);
    }
    
    private List<int> VisitedCells(int x, int y, bool remove = false)
    {
        var directions = Cells[x][y].ValidDirections();
        var newCellList = remove ? new List<int>(directions) : new List<int>();
        foreach (var dir in directions.Where(dir => GetNextCellUsingDirection(Cells[x][y], dir).IsVisited))
        {
            if (remove)
                newCellList.Remove(dir);
            else
                newCellList.Add(dir);
        }
        return newCellList;
    }
}