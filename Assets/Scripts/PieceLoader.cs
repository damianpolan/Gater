using UnityEngine;
using System.Collections;
using System;

public class PieceLoader
{
    public static GameObject CreateViewPiece(Piece piece)
    {
        string piece_prefab_path = "Prefabs/wall";
        string piece_sprite_path = "Sprites/";

        switch (piece.Symbol())
        {
            case EmptyPiece.symbol:
                return null;
            case WallPiece.symbol:
                piece_sprite_path += "GenericWall";
                break;
            case OnePiece.symbol:
                piece_sprite_path += "OnePiece";
                break;
            case TwoPiece.symbol:
                piece_sprite_path += "TwoPiece";
                break;
            case ThreePiece.symbol:
                piece_sprite_path += "ThreePiece";
                break;
            case FourPiece.symbol:
                piece_sprite_path += "FourPiece";
                break;
            default:
                throw new Exception("Piece class not found.");
        }

        GameObject obj = GameObject.Instantiate(Resources.Load(piece_prefab_path)) as GameObject;

        Sprite piece_sprite = Resources.Load(piece_sprite_path, typeof(Sprite)) as Sprite;
        obj.GetComponent<SpriteRenderer>().sprite = piece_sprite;
        obj.GetComponent<ViewPiece>().AssignPiece(piece);

        return obj;
    }

}
