using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GaterModel : GaterElement
{
    //public TextAsset mapAsset;

    public Board board;

    public delegate void MoveBlocked(Piece from_piece, Piece piece, int from_x, int from_y, int to_x, int to_y);
    public event MoveBlocked OnMoveBlocked;


    private void Awake()
    {
        board = new Board();
        TextAsset map = (TextAsset)Resources.Load(GameApplication.currentLevel, typeof(TextAsset));
        board.Load(map.text);
    }
    
    public GaterModel()
    {

    }

    public void MakeMove(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                Shift(0, -1);
                break;
            case Direction.Down:
                Shift(0, 1);
                break;
            case Direction.Left:
                Shift(-1, 0);
                break;
            case Direction.Right:
                Shift(1, 0);
                break;
        }
    }

    public bool IsGameOver()
    {
        Piece onePiece = board.FirstPieceWithSymbol(OnePiece.symbol);
        return (board[onePiece.X + 1, onePiece.Y].Symbol() == TwoPiece.symbol
            && board[onePiece.X, onePiece.Y + 1].Symbol() == ThreePiece.symbol
            && board[onePiece.X + 1, onePiece.Y + 1].Symbol() == FourPiece.symbol);
    }

    private void Shift(int amount_x, int amount_y)
    {
        IEnumerable<int> x_range = Enumerable.Range(0, board.Width);
        IEnumerable<int> y_range = Enumerable.Range(0, board.Height);

        if (amount_x > 0) x_range = x_range.Reverse();
        if (amount_y > 0) y_range = y_range.Reverse();

        foreach (int y in y_range)
            foreach (int x in x_range)
                if (board[x, y].IsMoveable())
                    MovePiece(x, y, x + amount_x, y + amount_y);
    }

    private void MovePiece(int from_x, int from_y, int to_x, int to_y)
    {
        Piece from_piece = board[from_x, from_y];
        Piece to_piece = board[to_x, to_y];

        if (board.IsInsideBounds(to_x, to_y) && !to_piece.IsBlocking())
        {
            board[to_x, to_y] = from_piece;
            from_piece.UpdatePosition(to_x, to_y);

            board[from_x, from_y] = new EmptyPiece(from_y, from_x);
        }
        else
        {
            if (OnMoveBlocked != null)
                OnMoveBlocked(from_piece, to_piece, from_x, from_y, to_x, to_y);
        }
    }
}

