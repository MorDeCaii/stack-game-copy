using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutPiece
{
    public Vector3 position;
    public Vector3 localScale;

    public CutPiece(Vector3 _position, Vector3 _scale)
    {
        position = _position;
        localScale = _scale;
    }
}
