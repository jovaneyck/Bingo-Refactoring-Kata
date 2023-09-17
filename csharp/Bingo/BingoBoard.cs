namespace Bingo;

public class BingoBoard
{

    private readonly string?[,] _cells;
    private readonly bool[,] _marked;

    public BingoBoard(int width, int height)
    {
        this._cells = new string[width, height];
        this._marked = new bool[width,height];
    }

    public void DefineCell(int x, int y, string value)
    {
        if (_cells[x,y] != null)
        {
            throw new ArgumentException("cell already defined");
        }
        for (var c = 0; c < _cells.GetLength(0); c++)
        {
            for (var r = 0; r < _cells.GetLength(1); r++)
            {
                if (value == _cells[c,r])
                    throw new ArgumentException(value + " already present at " + c + "," + r);
            }
        }
        _cells[x,y] = value;
    }

    public void MarkCell(int x, int y)
    {
        if (!IsInitialized())
        {
            throw new ArgumentException("board not initialized");
        }
        _marked[x,y] = true;
    }

    public bool IsMarked(int x, int y)
    {
        return _marked[x,y];
    }

    public bool IsInitialized()
    {
        foreach(var cell in _cells)
        {
            if (cell == null)
                    return false;
        }
        return true;
    }

}