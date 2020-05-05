using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public Brick prevBrick;
    public float spawnOffsetH;
    public float spawnOffsetV;

    [System.NonSerialized]
    public BrickAxis curAxis;

    private ColorPicker colorPicker;

    void Start()
    {
        curAxis = BrickAxis.Z;
        colorPicker = new ColorPicker();
    }

    public Brick SpawnBrick (Brick brick, Vector3 originPos, float moveSpeed, Transform parent = null)
    {
        Vector3 spawnPos;
        Vector3 prevBrickPos = prevBrick.transform.position;

        if (curAxis == BrickAxis.Z) curAxis = BrickAxis.X;
        else curAxis = BrickAxis.Z;

        if (curAxis == BrickAxis.Z) spawnPos = originPos + new Vector3 (0, prevBrickPos.y + spawnOffsetV, prevBrickPos.z + spawnOffsetH);
        else spawnPos = originPos + new Vector3 (prevBrickPos.x - spawnOffsetH, prevBrickPos.y + spawnOffsetV, 0);

        Brick newBrick = Instantiate(brick, spawnPos, Quaternion.identity, parent);
        Color color = colorPicker.GetNextColor();
        newBrick.Init(curAxis, color, moveSpeed);

        return newBrick;
    }
}
