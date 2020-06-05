using System;
using System.Collections;
using System.Collections.Generic;
using StaticClasses;
using Random = UnityEngine.Random;

namespace Algorithms
{
    public abstract class RandomWalkAlgo: AlgorithmBase
    {
        private int _visitedCellCtr = 1;
        protected int X;
        protected int Y;
        protected int NewX;
        protected int NewY;
        protected virtual bool ContinueWalkCondition => _visitedCellCtr < Height * Width;
        protected RandomWalkAlgo(int width, int height)  : base(width, height)
        {
        }
    
        protected override IEnumerator WalkThroughCellsCoRoutine(Action<int, int> fn)
        {
            SetInitialCellPosition(fn);
            while (ContinueWalkCondition)
            {
                var directions = GetValidDirections(X, Y);
                int direction = Utils.RandomSelectionFromArray(directions.ToArray());
                (NewX, NewY) = GetNextCellUsingDirection(X, Y, direction);
                if (WillDeleteWall(NewX, NewY))
                    yield return DeleteWall(X, Y, direction);
                (X, Y) = (NewX, NewY);
                fn(X, Y);
            }
        }

        protected abstract List<int> GetValidDirections(int x, int y);

        protected abstract bool WillDeleteWall(int x, int y);

        protected object DeleteWall(int x, int y, int direction)
        {
            Cells[x][y].DeleteWallWithPosition(direction);
            _visitedCellCtr++;
            return null;
        }
    
    

        protected virtual void SetInitialCellPosition(Action<int, int> fn)
        {
            X = Random.Range(1, Width);
            Y = Random.Range(1, Height);
            fn(X, Y);
        }
    
        protected override void EndLastItem(GridGeneratorBase mono, int x, int y)
        {
            if (_visitedCellCtr < Height * Width-1) return;
            base.EndLastItem(mono, x, y);
        }

        public override void Execute(GridGeneratorBase mono)
        {
            mono.StartCoroutine(WalkThroughCellsCoRoutine((x, y) =>
            {
                if (Cells[x][y].IsVisited) return;
                Cells[x][y].IsVisited = true;
                EndLastItem(mono, x, y);
            }));
        }

        protected Tuple<int, int> GetNextCellUsingDirection(int x, int y, int direction)
        {
            switch (direction)
            {
                case Constants.TOP:
                    y += 1;
                    break;
                case Constants.LEFT:
                    x -= 1;
                    break;
                case Constants.RIGHT:
                    x += 1;
                    break;
                case Constants.BOTTOM:
                    y -= 1;
                    break;
                default:
                    return null;
            }
            return new Tuple<int, int>(x, y);
        }
        protected Cell GetNextCellUsingDirection(Cell cell, int direction)
        {
            var (x, y) = GetNextCellUsingDirection(cell.Column, cell.Row, direction);
            return Cells[x][y];
        }
    }
}