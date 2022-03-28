using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sway : MonoBehaviour
{
    Transform trans;
    Vector3 origin;
    public Vector2 range;
    public Vector2 speed;
    public Vector2 period;
    int TICK = 0;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        origin=trans.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        trans.localPosition = origin + new Vector3(Mathf.Sin((TICK+period.x)*speed.x)*range.x,Mathf.Sin((TICK+period.y)*speed.y)*range.y,0);
        TICK++;
    }
}
