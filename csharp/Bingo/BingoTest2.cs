using Xunit;

namespace Bingo;

/**
 * This class is identical to {@link BingoTest} but all BDD-style methods are
 * inlined (and thus this test does depend at multiple points on the
 * implementation details of {@link BingoBoard})
 */
public class BingoTest2
{
    private BingoBoard board;

    [Fact]
    public void AnNewlyCreatedBoardIsNotInitialized()
    {
        board = new BingoBoard(1, 1);
        Assert.False(board.IsInitialized());
    }

    [Fact]
    public void WhenAllFieldsAreSetTheBoarIsInitialized()
    {
        var anyValue = "42";
        board = new BingoBoard(1, 1);
        board.DefineCell(0, 0, anyValue);
        Assert.True(board.IsInitialized());
    }

    [Fact]
    public void WhenAllFieldsOnRectangularBoardAreSetItIsInitialized()
    {
        var one = "0, 0";
        var two = "0, 1";
        board = new BingoBoard(1, 2);
        board.DefineCell(0, 0, one);
        board.DefineCell(0, 1, two);
        Assert.True(board.IsInitialized());
    }

    [Fact]
    public void ADefinedCellCantBeRedefinedEvenIfItsTheSameValue()
    {
        var anyValue = "42";
        board = new BingoBoard(1, 1);
        board.DefineCell(0, 0, anyValue);
        var e = Assert.Throws<ArgumentException>(() => { board.DefineCell(0, 0, anyValue); });
        Assert.Contains("already defined", e.Message);
    }

    [Fact]
    public void DuplicateCellsAreNotAllowed()
    {
        var anyValue = "42";
        board = new BingoBoard(2, 2);
        board.DefineCell(0, 1, anyValue);
        var e = Assert.Throws<ArgumentException>(() => { board.DefineCell(1, 0, anyValue); });
        Assert.Contains(" already present at 0,1", e.Message);
    }

    [Fact]
    public void ANonInitializedBoardCannotBeMarked()
    {
        board = new BingoBoard(1, 1);
        var e = Assert.Throws<ArgumentException>(() => { board.MarkCell(0, 0); });
        Assert.Contains("not initialized", e.Message);
    }

    [Fact]
    public void WhenAllCellGetsMarkedItIsMarked()
    {
        var anyValue = "42";
        board = new BingoBoard(1, 1);
        board.DefineCell(0, 0, anyValue);
        board.MarkCell(0, 0);
        Assert.True(board.IsMarked(0, 0));
    }
}