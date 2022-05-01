using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class train_wheel : MonoBehaviour
{
    Transform trans;
    Transform trainTrans;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.transform;
        trainTrans = GameObject.Find("train").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        trans.eulerAngles = new Vector3(0, 0, trainTrans.position.x*-125);
    }
}
