using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutResult
{
    public bool isSuccess;
    public CutPiece cutPiece;
    public BrickAxis axis;

    CutResult(bool _isSuccess, CutPiece _cutPiece, BrickAxis _axis)
    {
        isSuccess = _isSuccess;
        cutPiece = _cutPiece;
        axis = _axis;
    }

    public static CutResult Fail(BrickAxis axis)
    {
        return new CutResult(false, null, axis);
    }
    public static CutResult Success(BrickAxis axis)
    {
        return new CutResult(true, null, axis);
    }
    public static CutResult Partial(Vector3 position, Vector3 scale, BrickAxis axis)
    {
        return new CutResult(true, new CutPiece(position, scale), axis);
    }
}
