using System.Collections.Generic;

public class Cell
{
    private readonly Wall[] _walls;
    public int Row { get; }
    public int Column { get; }
    public bool IsVisited { get; set; }
    public Cell(int column, int row)
    {
        _walls = new Wall[4];
        Row = row;
        Column = column;
    }

    public List<int> ValidDirections()
    {
        List<int> validDirections = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            if (!_walls[i].IsEdge) validDirections.Add(i);
        }
        return validDirections;
    }

    /// <summary>
    /// Call only after maze is completely generated
    /// </summary>
    public List<int> ValidDirectionsForDijkstra()
    {
        List<int> directions = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            if (_walls[i].IsDeleted) directions.Add(i);
        }
        return directions;
    }

    public void AddWall(Wall wall, int i)
    {
        _walls[i] = wall;
    }

    public void DeleteWallWithPosition(int position)
    {
        _walls[position].DeleteWall();
        //_walls[position] = null;
    }

    public void Destroy()
    {
        foreach (var wall in _walls)
            wall.DeleteWall(true);
    }
}
