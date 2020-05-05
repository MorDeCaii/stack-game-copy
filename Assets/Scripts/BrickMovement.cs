using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMovement : IMovement
{
    public bool isMoving;

    private float speed;
    private Vector3 startPos;
    public Vector3 endPos;
    private Transform transform;
    private BrickAxis axis;
    private Vector3 targetPos;

    public BrickMovement (Transform _transform, float _speed, Vector3 _startPos, BrickAxis _axis)
    {
        speed = _speed;
        startPos = _startPos;
        transform = _transform;
        axis = _axis;
        isMoving = true;

        if (axis == BrickAxis.X) endPos = new Vector3(-startPos.x, startPos.y, startPos.z);
        else endPos = new Vector3(startPos.x, startPos.y, -startPos.z);

        targetPos = endPos;
    }

    public void Move()
    {
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

            if (transform.position == targetPos)
            {
                if (targetPos == endPos) targetPos = startPos;
                else targetPos = endPos;
            }
        }
    }

    public void SetSpeed (float _speed)
    {
        speed = _speed;
    }

    public void SetAxis (BrickAxis _axis)
    {
        axis = _axis;
    }

    public void ToggletMovement()
    {
        isMoving = !isMoving;
    }
}
