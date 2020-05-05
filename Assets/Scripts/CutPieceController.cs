using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutPieceController : MonoBehaviour
{
    private Transform camera;
    private Rigidbody rb;

    void Start()
    {
        camera = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (camera.position.y - transform.position.y > 40)
        {
            Destroy (gameObject);
        }
    }
}
