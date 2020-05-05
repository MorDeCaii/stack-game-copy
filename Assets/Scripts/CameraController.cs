using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public float verticalOffset;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FocusOnBrick (Brick brick)
    {
        transform.DOMoveY(brick.transform.position.y + verticalOffset, 0.4f).SetEase(Ease.OutQuart);
    }
}
