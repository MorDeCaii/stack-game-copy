using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Brick : MonoBehaviour
{
    // Spawn next brick
    // Place brick on click and cut it

    public Color color;
    public IMovement movement;

    private Mesh mesh;
    private List<Vector3> vertices;
    private List<Vector2> points = new List<Vector2>();

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        points = GetPoints();
    }

    void Update()
    {
        if (movement != null) movement.Move();
        points = GetPoints();
    }

    public void Init (BrickAxis _axis, Color _color, float speed)
    {
        Renderer renderer = GetComponent<Renderer>();
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetColor("_BaseColor", _color);
        renderer.SetPropertyBlock(propertyBlock);
        color = _color;

        movement = new BrickMovement(transform, speed, transform.position, _axis);
    }

    List<Vector2> GetPoints()
    {
        vertices = mesh.vertices.ToList();
        List<Vector2> points = new List<Vector2>();

        foreach (Vector3 vert in vertices)
        {
            Vector3 worldPos = transform.TransformPoint(vert);
            Vector2 pointToAdd = new Vector2(worldPos.x, worldPos.z);
            if (!points.Contains(pointToAdd)) points.Add(pointToAdd);
        }

        return points;
    }

    // Get point by number (1-4)
    public Vector2 GetPointByNumber(int number)
    {
        switch (number)
        {
            case 1:
                return points.Where(p => p.y < transform.position.z).Where(p => p.x < transform.position.x).First();
            case 2:
                return points.Where(p => p.y > transform.position.z).Where(p => p.x < transform.position.x).First();
            case 3:
                return points.Where(p => p.y > transform.position.z).Where(p => p.x > transform.position.x).First();
            case 4:
                return points.Where(p => p.y < transform.position.z).Where(p => p.x > transform.position.x).First();
            default:
                return points.Where(p => p.y < transform.position.z).Where(p => p.x < transform.position.x).First();
        }
    }

    public void MakeStatic()
    {
        GetComponent<Brick>().enabled = false;
        gameObject.isStatic = true;
    }
}
