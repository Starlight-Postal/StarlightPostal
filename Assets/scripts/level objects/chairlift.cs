using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chairlift : MonoBehaviour
{
    public LineRenderer stroke;
    public EdgeCollider2D collider;
    public Vector2[] path;
    // Start is called before the first frame update
    void Start()
    {
        stroke = gameObject.GetComponent<LineRenderer>();
        Vector3[] p = new Vector3[stroke.positionCount];
        stroke.GetPositions(p);
        path = new Vector2[p.Length];
        for (int i = 0; i < p.Length; i++)
        {
            path[i] = new Vector2(p[i].x, p[i].y);
        }
        collider.points = path;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
