using System.Collections.Generic;
using UnityEngine;

public class Map : GaterElement
{
    public Vector2 position;
    public float bound_width = 2;
    public float bound_height = 2;

    public Camera game_camera;


    public float pieceWidth = 1; // use (1, 1) as the size of each piece
    public float pieceHeight = 1;
    public float total_width;
    public float total_height;
    public float xMin;
    public float yMin;

    string noMoveSignal_prefab_path = "Prefabs/NoMoveSignal";
    List<GameObject> noMoveParticles = new List<GameObject>();

    void Start()
    {
        Piece[] pieces = app.model.board.Pieces();

        List<GameObject> object_pieces = new List<GameObject>();

        total_width = pieceWidth * app.model.board.Width;
        total_height = pieceHeight * app.model.board.Height;
        xMin = position.x - total_width / 2;
        yMin = position.y - total_height / 2;

        foreach (Piece piece in pieces)
        {
            GameObject obj = PieceLoader.CreateViewPiece(piece);

            if (obj == null) //No piece (empty tile)
                continue;

            Bounds bounds = obj.GetComponent<SpriteRenderer>().bounds;
            obj.transform.localScale = new Vector3(
                pieceWidth / bounds.size.x,
                pieceHeight / bounds.size.y,
                0);

            obj.GetComponent<ViewPiece>().SetPosition(WorldPositionFor(piece.X, piece.Y));
            obj.transform.parent = this.gameObject.transform;

            object_pieces.Add(obj);
        }

        //Attach to move blocked listener
        app.model.OnMoveBlocked += MoveBlocked;

        //Fit Camera
        float target_camera_width = total_width + bound_width;
        float target_camera_height = total_height + bound_height;
        if (Camera.main.aspect > 1)
            game_camera.orthographicSize = target_camera_height / 2;
        else
            game_camera.orthographicSize = (target_camera_width / game_camera.aspect) / 2;
    }

    public void MoveBlocked(Piece from_piece, Piece to_piece, int from_x, int from_y, int to_x, int to_y)
    {
        if (!to_piece.IsMoveable())
        {
            Vector3 from_pos = WorldPositionFor(from_x, from_y);
            Vector3 to_pos = WorldPositionFor(to_x, to_y);
            Vector3 relative_position = to_pos - from_pos;
            //Assumes pieces only ever move a manhatten distance of 1
            Vector3 particle_position = new Vector3(
                from_pos.x + relative_position.x / 2.0f,
                from_pos.y + relative_position.y / 2.0f, -1);

            GameObject obj = GameObject.Instantiate(Resources.Load(noMoveSignal_prefab_path)) as GameObject;
            noMoveParticles.Add(obj);
            ParticleSystem particle = obj.GetComponent<ParticleSystem>();

            obj.transform.position = particle_position;

            if (relative_position.x != 0) //blocked attempted a horizontal move
            {
                particle.startRotation = 0;
            }
            else if (relative_position.y != 0)
            {
                particle.startRotation = Mathf.Deg2Rad * 90f;
            }
            else { Debug.Log("Warning! No relative position change found"); }

            particle.Emit(1);
        }
    }

    void Update()
    {
        for (int i = 0; i < noMoveParticles.Count && noMoveParticles.Count > 0; i++)
            if (!noMoveParticles[i].GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(noMoveParticles[i]);
                noMoveParticles.RemoveAt(i);
                i--;
            }
    }

    public Vector3 WorldPositionFor(int game_x, int game_y)
    {
        float position_x = xMin + game_x * pieceWidth + pieceWidth / 2;
        float position_y = yMin + (app.model.board.Height - game_y) * pieceHeight - pieceHeight / 2;
        return new Vector3(position_x, position_y, 0);
    }

}
