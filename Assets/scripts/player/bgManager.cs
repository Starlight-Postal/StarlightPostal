using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgManager : MonoBehaviour
{
    public Transform trans;
    public Transform[] layers;
    public float[] depths;
    public Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        trans=gameObject.GetComponent<Transform>();
        camera = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < layers.Length;i++)
        {
            //layers[i].localScale = new Vector3(1, 1, 1) * ((1-depths[i])+depths[i]*0.1f);
            layers[i].localPosition = new Vector3(-(trans.position.x-camera.position.x) * depths[i], -(trans.position.y-camera.position.y) * depths[i], layers[i].localPosition.z);
        }
    }
}
