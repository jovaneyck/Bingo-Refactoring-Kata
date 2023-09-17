using Xunit;

namespace Bingo;

public class BingoTest
{
    private BingoBoard? _board;

    [Fact]
    public void ANewlyCreatedBoardIsNotInitialized()
    {
        GivenBingoBoardOfSize(1, 1);
        ThenBoardIsNotInitialized();
    }

    [Fact]
    public void WhenAllFieldsAreSetTheBoardIsInitialized()
    {
        var anyValue = "42";
        GivenBingoBoardOfSize(1, 1);
        WhenCellIsDefined(0, 0, anyValue);
        ThenBoardIsInitialized();
    }

    [Fact]
    public void WhenAllFieldsOnRectangularBoardAreSetItIsInitialized()
    {
        var one = "0, 0";
        var two = "0, 1";
        GivenBingoBoardOfSize(1, 2);
        WhenCellIsDefined(0, 0, one);
        WhenCellIsDefined(0, 1, two);
        ThenBoardIsInitialized();
    }

    [Fact]
    public void ADefinedCellCantBeRedefinedEvenIfItsTheSameValue()
    {
        var anyValue = "42";
        GivenBingoBoardOfSize(1, 1);
        WhenCellIsDefined(0, 0, anyValue);
        var e = Assert.Throws<ArgumentException>(() => WhenCellIsDefined(0, 0, anyValue));
        Assert.Contains("already defined", e.Message);
    }

    [Fact]
    public void DuplicateCellsAreNotAllowed()
    {
        var anyValue = "42";
        GivenBingoBoardOfSize(2, 2);
        WhenCellIsDefined(0, 1, anyValue);

        var e = Assert.Throws<ArgumentException>(() => WhenCellIsDefined(1, 0, anyValue));
        Assert.Contains("already present at 0,1", e.Message);
    }

    [Fact]
    public void ANonInitializedBoardCannotBeMarked()
    {
        GivenBingoBoardOfSize(1, 1);
        var e = Assert.Throws<ArgumentException>(() => WhenCellIsMarked(0, 0));
        Assert.Contains("not initialized", e.Message);
    }

    [Fact]
    private void WhenAllCellGetsMarkedItIsMarked()
    {
        var anyValue = "42";
        GivenBingoBoardOfSize(1, 1);
        WhenCellIsDefined(0, 0, anyValue);
        WhenCellIsMarked(0, 0);
        ThenCellIsMarked(0, 0);
    }

    private void GivenBingoBoardOfSize(int width, int height)
    {
        _board = new BingoBoard(width, height);
    }

    private void WhenCellIsDefined(int x, int y, string value)
    {
        _board!.DefineCell(x, y, value);
    }

    private void WhenCellIsMarked(int x, int y)
    {
        _board!.MarkCell(x, y);
    }

    private void ThenBoardIsNotInitialized()
    {
        Assert.False(BoardInitializeState());
    }

    private void ThenBoardIsInitialized()
    {
        Assert.True(BoardInitializeState());
    }

    private bool BoardInitializeState()
    {
        return _board!.IsInitialized();
    }

    private void ThenCellIsMarked(int x, int y)
    {
        Assert.True(_board!.IsMarked(x, y));
    }
}