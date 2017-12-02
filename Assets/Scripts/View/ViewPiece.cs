using UnityEngine;
using System.Collections;

public class ViewPiece : GaterElement
{
    public Piece piece;

    Mover mover;

    public float moveTime = 0.1f;

    void Start()
    {
        mover = new Mover(transform);
    }

    public void AssignPiece(Piece piece)
    {
        this.piece = piece;
        piece.OnMoved += PieceOnMoved;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void PieceOnMoved(int from_x, int from_y, int to_x, int to_y)
    {
        mover.MoveTo(app.view.WorldPositionFor(to_x, to_y), moveTime);
    }

    void Update()
    {
        if (mover != null)
            mover.Update();
    }
}
