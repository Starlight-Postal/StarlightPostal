using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lift_chair : MonoBehaviour
{
    public chairlift chairlift;
    public int node = 0;
    public float speed;
    Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 t = chairlift.path[(node + 1) % chairlift.path.Length];
        Vector2 d = t - new Vector2(trans.position.x, trans.position.y);
        if (d.magnitude > speed)
        {
            trans.position += (Vector3)(d / d.magnitude * speed);
        } else
        {
            node = (node + 1) % chairlift.path.Length;
            trans.position += (Vector3)d;
            if (node == chairlift.path.Length-1)
            {
                node = 0;
                trans.position = new Vector3(chairlift.path[node].x, chairlift.path[node].y, trans.position.z);
            }
            
        }
    }
}
