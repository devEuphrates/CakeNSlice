using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeSliceLayer : MonoBehaviour
{
    List<CakeSlicePiece> _pieces = new List<CakeSlicePiece>();
    public float Percent = 0f;

    public void AddPiece(CakeSlicePiece piece, float pieceSize)
    {
        _pieces.Add(piece);
        Percent += pieceSize;

        piece.transform.SetParent(transform);
    }
}
