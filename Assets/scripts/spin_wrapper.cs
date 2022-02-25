using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin_wrapper : MonoBehaviour
{
    public Transform trans;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        trans.eulerAngles += new Vector3(0, 0, speed);
    }
}
