using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind_tunnel : wind_structure
{
    public LineRenderer stroke;
    public Vector2[] path;
    public float power = 0;
    public float blur = 0.5f;
    public float weight = 5;

    public Transform trans;

    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        stroke = gameObject.GetComponent<LineRenderer>();
        Vector3[] p = new Vector3[stroke.positionCount];
        stroke.GetPositions(p);
        path = new Vector2[p.Length];
        for(int i = 0;i < p.Length;i++)
        {
            path[i] = new Vector2(p[i].x+trans.position.x, p[i].y+trans.position.y);
        }
        stroke.enabled = false;
    }

    public override Vector2 getWind(float x, float y)
    {
        Vector2 wind = new Vector2(0, 0);
        for(int i = 0;i < path.Length-1;i++)
        {
            Vector2 p = pointToSeg(path[i], path[i + 1], new Vector2(x, y));
            float d = 1-((Vector2.Distance(new Vector2(x, y), p))/ (weight * 0.5f));
            if (d >= 0)
            {
                float b = 1;
                if (blur > 0)
                {
                    b = d/blur;
                    if (b > 1)
                    {
                        b = 1;
                    }
                    if (b < 0)
                    {
                        b = 0;
                    }
                }
                Vector2 delta = path[i + 1] - path[i];
                delta.Normalize();
                wind += delta * power*b;
            }
            
        }
        return wind;
    }
    public static Vector2 pointToSeg(Vector2 linePnt, Vector2 p2, Vector2 pnt)
    {
        Vector2 lineDir = p2 - linePnt;
        lineDir.Normalize();//this needs to be a unit vector
        var v = pnt - linePnt;
        var d = Vector2.Dot(v, lineDir);
        Vector2 p = linePnt + lineDir * d;
        if (linePnt.x > p2.x)
        {
            if (p.x > linePnt.x)
            {
                p = linePnt;
            }
            if (p.x < p2.x)
            {
                p = p2;
            }
        } else
        {
            if (p.x < linePnt.x)
            {
                p = linePnt;
            }
            if (p.x > p2.x)
            {
                p = p2;
            }
        }
        if (linePnt.y > p2.y)
        {
            if (p.y > linePnt.y)
            {
                p = linePnt;
            }
            if (p.y < p2.y)
            {
                p = p2;
            }
        }
        else
        {
            if (p.y < linePnt.y)
            {
                p = linePnt;
            }
            if (p.y > p2.y)
            {
                p = p2;
            }
        }
        return p;
    }
}
