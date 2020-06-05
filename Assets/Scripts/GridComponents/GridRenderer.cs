using System;
using Algorithms;
using UnityEngine;

public class GridRenderer
{
    private int _height;
    private int _width;
    private Cell[][] _cells;
    private IWallInstantiable _prefab;
    private Transform _parent;

    private static GridRenderer _instance;

    public static GridRenderer Instance => _instance ?? Instantiate();

    public GridRenderer SetGrid(GridBase gridBase)
    {
        _cells = gridBase.Cells;
        _height = gridBase.Height;
        _width = gridBase.Width;
        return Instance;
    }

    private void ResetMembers()
    {
        _cells = null;
        _prefab = null;
        _parent = null;
    }

    private static GridRenderer Instantiate()
    {
        _instance = new GridRenderer();
        return _instance;
    }
    public void RenderWithPrefab(IWallInstantiable prefab)
    {
        _prefab = prefab;
        LoopThroughCells(CreateWalls);
        ResetMembers();
    }

    private void LoopThroughCells(Action<int, int> fn)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                fn(x, y);
            }
        }
    }
    private void CreateWalls(int x, int y)
    {
        for (int wallPosition = 0; wallPosition < 4; wallPosition++)
        {
            int newX = x;
            int newY = y;
            bool isEdge = false;
            bool isVertical = false;
            switch (wallPosition)
            {
                case Constants.BOTTOM:
                    isEdge = IsBottomOrLeftMostCell(y);
                    if (!isEdge) continue;
                    break;
                case Constants.LEFT:
                    isEdge = IsBottomOrLeftMostCell(x);
                    if (!isEdge) continue;
                    isVertical = true;
                    break;
                case Constants.RIGHT:
                    newX = x + 1;
                    isEdge = IsRightMostCell(x);
                    isVertical = true;
                    break;
                case Constants.TOP:
                    newY = y + 1;
                    isEdge = IsTopCell(y);
                    break;
            }

            var t = _prefab.Instantiate(newX, newY, _parent, isVertical);
            var wall = new Wall(t, isEdge);
            //t.SetParent(_parent, false);
            AddWallToCell(x, y, wall, wallPosition, isEdge);
        }
    }
    private void AddWallToCell(int x, int y, Wall wall, int wallPosition, bool isEdge)
    {
        _cells[x][y].AddWall(wall, wallPosition);
        if (isEdge) return;
        if (!IsTopCell(y) && wallPosition == Constants.TOP)
            _cells[x][y + 1].AddWall(wall, Constants.BOTTOM);
        if (!IsRightMostCell(x) && wallPosition == Constants.RIGHT)
            _cells[x + 1][y].AddWall(wall, Constants.LEFT);
    }

    public static void DestroyGrid(GridBase gridBase)
    {
        if (gridBase == null) return;
        foreach (var cellGroup in gridBase.Cells)
        {
            foreach (var cell in cellGroup)
            {
                cell.Destroy();
            }
        }
    }

    public GridRenderer ParentToTransform(Transform t)
    {
        _parent = t;
        return _instance;
    }
    
    private bool IsBottomOrLeftMostCell(int i)
    {
        return i == 0;
    }
    private bool IsTopCell(int y)
    {
        return y == _height - 1;
    }
    private bool IsRightMostCell(int x)
    {
        return x == _width - 1;
    }
}
