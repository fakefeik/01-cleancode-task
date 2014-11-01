using System.Linq;

namespace CleanCode
{
    public enum Status
    {
        Check,
        Mate, 
        Ok,
        Stalemate
    }

	public class Chess
	{
		private readonly Board board;

		public Chess(Board board)
		{
		    this.board = board;
		}

	    public Status GetWhiteStatus()
	    {
	        var isWhiteChecked = CheckForWhite();
	        var uncheckingMoveExists = false;
	        foreach (var whiteLoc in board.Figures(Cell.White))
	            foreach (var nextLoc in board.Get(whiteLoc).Figure.Moves(whiteLoc, board))
	            {
	                var oldDest = board.PerformMove(whiteLoc, nextLoc);
	                if (!CheckForWhite())
	                    uncheckingMoveExists = true;
	                board.PerformUndoMove(whiteLoc, nextLoc, oldDest);
	            }

	        if (isWhiteChecked)
	            return uncheckingMoveExists ? Status.Check : Status.Mate;
	        return uncheckingMoveExists ? Status.Ok : Status.Stalemate;
	    }

	    private bool CheckForWhite()
	    {
	        return board
                .Figures(Cell.Black)
                .Any(loc => board.Get(loc)
                    .Figure
                    .Moves(loc, board)
                    .Any(x => board.Get(x).IsWhiteKing));
	    }
	}
}