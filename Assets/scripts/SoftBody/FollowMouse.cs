using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    public float speed = 10.0F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mcam = GameObject.Find("Main Camera").GetComponent(typeof(Camera)) as Camera;
        Vector3 mousePosition = mcam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0F;
        var dir = mousePosition - transform.position;
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }
}
