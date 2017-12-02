using UnityEngine;
using System.Collections;

public abstract class Piece
{
    protected int x;
    protected int y;

    public int X { get { return x; } }
    public int Y { get { return y; } }

    //Events
    public delegate void MoveAction(int from_x, int from_y, int to_x, int to_y);
    public event MoveAction OnMoved;

    protected Piece()
    {
    }

    protected Piece(int x, int y)
    {
        this.x = x;
        this.y = y;
    }



    public abstract char Symbol();
    public abstract bool IsMoveable();
    public abstract bool IsBlocking();
    


    public void UpdatePosition(int new_x, int new_y)
    {
        int from_x = x;
        int from_y = y;

        x = new_x;
        y = new_y;

        if (OnMoved != null)
            OnMoved(from_x, from_y, new_x, new_y);
    }

    public static Piece PieceFor(char piece_character)
    {
        switch (piece_character)
        {
            case EmptyPiece.symbol:
                return new EmptyPiece();
            case WallPiece.symbol:
                return new WallPiece();
            case OnePiece.symbol:
                return new OnePiece();
            case TwoPiece.symbol:
                return new TwoPiece();
            case ThreePiece.symbol:
                return new ThreePiece();
            case FourPiece.symbol:
                return new FourPiece();
            default:
                return null;
        }
    }
}

public class EmptyPiece : Piece
{
    public const char symbol = '-';

    public EmptyPiece() : base() { }
    public EmptyPiece(int x, int y) : base(x, y) { }

    public override char Symbol() { return symbol; }
    public override bool IsBlocking() { return false; }
    public override bool IsMoveable() { return false; }
}

public class WallPiece : Piece
{
    public const char symbol = '#';

    public WallPiece() : base() { }
    public WallPiece(int x, int y) : base(x, y) { }

    public override char Symbol() { return symbol; }
    public override bool IsBlocking() { return true; }
    public override bool IsMoveable() { return false; }
}

public class OnePiece : Piece
{
    public const char symbol = '1';

    public OnePiece() : base() { }
    public OnePiece(int x, int y) : base(x, y) { }

    public override char Symbol() { return symbol; }
    public override bool IsBlocking() { return true; }
    public override bool IsMoveable() { return true; }
}

public class TwoPiece : Piece
{
    public const char symbol = '2';

    public TwoPiece() : base() { }
    public TwoPiece(int x, int y) : base(x, y) { }

    public override char Symbol() { return symbol; }
    public override bool IsBlocking() { return true; }
    public override bool IsMoveable() { return true; }
}

public class ThreePiece : Piece
{
    public const char symbol = '3';

    public ThreePiece() : base() { }
    public ThreePiece(int x, int y) : base(x, y) { }

    public override char Symbol() { return symbol; }
    public override bool IsBlocking() { return true; }
    public override bool IsMoveable() { return true; }
}

public class FourPiece : Piece
{
    public const char symbol = '4';

    public FourPiece() : base() { }
    public FourPiece(int x, int y) : base(x, y) { }

    public override char Symbol() { return symbol; }
    public override bool IsBlocking() { return true; }
    public override bool IsMoveable() { return true; }
}