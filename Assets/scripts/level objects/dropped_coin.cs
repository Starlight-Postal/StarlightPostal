using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropped_coin : MonoBehaviour
{
    Vector2 velocity;
    public float p = 1;
    public float grav = 0.1f;
    Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(Random.Range(-p, p), Random.Range(0, p*2));
        trans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity += new Vector2(0,-grav);
        trans.position += new Vector3(velocity.x, velocity.y, 0);
    }
}
