public class GridBase
{
    public Cell[][] Cells { get; private set; }
    public int Width { get; }
    public int Height{ get; }

    protected GridBase(int width, int height)
    {
        Width = width;
        Height = height;
        InstantiateCells(width, height);
    }
    private void InstantiateCells(int x, int y)
    {
        Cells = new Cell[x][];
        for (int i = 0; i < x; i++)
        {
            Cells[i] = new Cell[y];
            for (int j = 0; j < y; j++)
            {
                Cells[i][j] = new Cell(i, j);
            }
        }
    }
}
