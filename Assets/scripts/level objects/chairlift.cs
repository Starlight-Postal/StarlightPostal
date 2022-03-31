using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chairlift : MonoBehaviour
{
    public LineRenderer stroke;
    public EdgeCollider2D collider;
    public Vector2[] path;
    float length;
    float[] lengths;

    public float speed;
    public int chairs = 10;
    public GameObject chair;
    // Start is called before the first frame update
    void Start()
    {
        stroke = gameObject.GetComponent<LineRenderer>();
        Vector3[] p = new Vector3[stroke.positionCount];
        stroke.GetPositions(p);
        path = new Vector2[p.Length];
        lengths = new float[p.Length - 1];
        for (int i = 0; i < p.Length; i++)
        {
            path[i] = new Vector2(p[i].x, p[i].y);
            if (i > 0)
            {
                lengths[i-1]= (p[i] - p[i - 1]).magnitude;
                length += (p[i]-p[i-1]).magnitude;
                //Debug.Log(lengths[i - 1]);
            }
        }
        //Debug.Log(length);
        collider.points = path;
        float space = length / chairs;
        int n = 0;
        //int dist = 0;
        Vector2 dir = (path[1] - path[0]) / (path[1] - path[0]).magnitude;
        Vector2 pos = path[0];
        for(int i = 0;i < chairs;i++)
        {
            //Debug.Log(new Vector3(pos.x, pos.y, 0));
            GameObject c = Instantiate(chair, new Vector3(pos.x,pos.y,1), Quaternion.identity);
            //Debug.Log("chair at "+n);
            lift_chair thisChair = c.GetComponent<lift_chair>();
            thisChair.node = n;
            thisChair.chairlift = this;
            thisChair.speed = speed;
            float move = space;
            while((path[(n + 1) % path.Length] - pos).magnitude < move)
            {
                n = (n + 1) % path.Length;
                //Debug.Log(n);
                move -= (path[n] - pos).magnitude;
                pos = path[n];
                dir = (path[n + 1] - path[n]) / lengths[n];
                
            }

            pos += dir * move;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
