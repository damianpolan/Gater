using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class Board
{
    protected Piece[,] board;
    protected int width;
    protected int height;

    public int Width
    {
        get
        {
            return width;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }
    }

    public Board()
    {
    }

    public void Load(string text)
    {
        string[] lines = text.Split(new[] { "\r\n" }, StringSplitOptions.None);
        height = lines.Length;
        width = lines[0].Length;
        board = new Piece[lines.Length, lines[0].Length];
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
            {
                this[x, y] = Piece.PieceFor(lines[y][x]);
            }
    }

    public Piece this[int x, int y]
    {
        get
        {
            if (IsInsideBounds(x, y))
                return board[y, x];
            else
                return null;
        }
        set
        {
            if (value == null || !value.GetType().IsSubclassOf(typeof(Piece)))
                throw new Exception("Class must be of type Piece.");
            else if (IsInsideBounds(x, y))
            {
                board[y, x] = value;
                value.UpdatePosition(x, y);
            }
        }
    }

    public bool IsInsideBounds(int x, int y)
    {
        return y >= 0 && y < Height && x >= 0 && x < Width;
    }

    public Piece FirstPieceWithSymbol(char symbol)
    {
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Height; x++)
                if (this[x, y].Symbol() == symbol) return this[x, y];
        return null;
    }

    public Piece[] Pieces()
    {
        return board.Cast<Piece>().ToArray();
    }

    public override string ToString()
    {
        string s = "";
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Height; x++)
                s += board[x, y].Symbol();
            s += "\n";
        }
        return s;
    }
}
