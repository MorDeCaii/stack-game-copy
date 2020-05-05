using UnityEngine;

public class BrickCutter
{
    public static CutResult CutBrick (Brick brickToCut, Brick brick, BrickAxis axis, float threshold)
    {
        if (axis == BrickAxis.X)
        {
            Vector2 brickToCut_A = brickToCut.GetPointByNumber(1);
            Vector2 brickToCut_B = brickToCut.GetPointByNumber(4);

            Vector2 brick_A = brick.GetPointByNumber(1);
            Vector2 brick_B = brick.GetPointByNumber(4);

            if (brickToCut_B.x < brick_A.x || brickToCut_A.x > brick_B.x)
            {
                // Player missed!
                return CutResult.Fail(axis);
            }
            if (brickToCut_B.x < brick_B.x)
            {
                if (brick_B.x - brickToCut_B.x <= threshold)
                {
                    return CutResult.Success(axis);
                }
                else
                {
                    float newScaleX = (brickToCut_B.x - brick_A.x);
                    float newPosX = brickToCut_B.x - newScaleX / 2;

                    Vector3 brickScale = brickToCut.transform.localScale;
                    brickToCut.transform.localScale = new Vector3(newScaleX, brickScale.y, brickScale.z);

                    Vector3 brickPos = brickToCut.transform.position;
                    brickToCut.transform.position = new Vector3(newPosX, brickPos.y, brickPos.z);

                    Vector3 pieceScale = new Vector3(brick_A.x - brickToCut_A.x, brickScale.y, brickScale.z);
                    Vector3 piecePos = new Vector3(brickToCut_A.x + pieceScale.x / 2, brickPos.y, brickPos.z);
                    return CutResult.Partial (piecePos, pieceScale, axis);
                }
            }
            if (brickToCut_A.x > brick_A.x)
            {
                if (brickToCut_A.x - brick_A.x <= threshold)
                {
                    return CutResult.Success(axis);
                }
                else
                {
                    float newScaleX = (brick_B.x - brickToCut_A.x);
                    float newPosX = brickToCut_A.x + newScaleX / 2;

                    Vector3 brickScale = brickToCut.transform.localScale;
                    brickToCut.transform.localScale = new Vector3(newScaleX, brickScale.y, brickScale.z);

                    Vector3 brickPos = brickToCut.transform.position;
                    brickToCut.transform.position = new Vector3(newPosX, brickPos.y, brickPos.z);

                    Vector3 pieceScale = new Vector3(brickToCut_A.x - brick_A.x, brickScale.y, brickScale.z);
                    Vector3 piecePos = new Vector3(brick_B.x + pieceScale.x / 2, brickPos.y, brickPos.z);
                    return CutResult.Partial (piecePos, pieceScale, axis);
                }
            }
        }
        if (axis == BrickAxis.Z)
        {
            Vector2 brickToCut_A = brickToCut.GetPointByNumber(3);
            Vector2 brickToCut_B = brickToCut.GetPointByNumber(4);

            Vector2 brick_A = brick.GetPointByNumber(3);
            Vector2 brick_B = brick.GetPointByNumber(4);

            if (brickToCut_B.y > brick_A.y || brickToCut_A.y < brick_B.y)
            {
                // Player missed
                return CutResult.Fail(axis);
            }
            if (brickToCut_B.y > brick_B.y)
            {
                if (brickToCut_B.y - brick_B.y <= threshold)
                {
                    return CutResult.Success(axis);
                }
                else
                {
                    float newScaleZ = (brick_A.y - brickToCut_B.y);
                    float newPosZ = brickToCut_B.y + newScaleZ / 2;

                    Vector3 brickScale = brickToCut.transform.localScale;
                    brickToCut.transform.localScale = new Vector3(brickScale.x, brickScale.y, newScaleZ);

                    Vector3 brickPos = brickToCut.transform.position;
                    brickToCut.transform.position = new Vector3(brickPos.x, brickPos.y, newPosZ);

                    Vector3 pieceScale = new Vector3(brickScale.x, brickScale.y, brickToCut_A.y - brick_A.y);
                    Vector3 piecePos = new Vector3(brickPos.x, brickPos.y, brick_A.y + pieceScale.z / 2);
                    return CutResult.Partial(piecePos, pieceScale, axis);
                }
            }
            if (brickToCut_A.y < brick_A.y)
            {
                if (brick_A.y - brickToCut_A.y <= threshold)
                {
                    return CutResult.Success(axis);
                }
                else
                {
                    float newScaleZ = (brickToCut_A.y - brick_B.y);
                    float newPosZ = brickToCut_A.y - newScaleZ / 2;

                    Vector3 brickScale = brickToCut.transform.localScale;
                    brickToCut.transform.localScale = new Vector3(brickScale.x, brickScale.y, newScaleZ);

                    Vector3 brickPos = brickToCut.transform.position;
                    brickToCut.transform.position = new Vector3(brickPos.x, brickPos.y, newPosZ);

                    Vector3 pieceScale = new Vector3(brickScale.x, brickScale.y, brick_B.y - brickToCut_B.y);
                    Vector3 piecePos = new Vector3(brickPos.x, brickPos.y, brick_B.y - pieceScale.z / 2);
                    return CutResult.Partial(piecePos, pieceScale, axis);
                }
            }
        }
        return CutResult.Fail(axis);
    }
}
