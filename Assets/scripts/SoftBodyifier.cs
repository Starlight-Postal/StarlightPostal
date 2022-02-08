using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBodyifier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject boneContainer = transform.Find("Bones").gameObject;

        int numberOfBones = boneContainer.transform.childCount;

        for (int i = 0; i < numberOfBones; i++) {
            var bone = boneContainer.transform.GetChild(i).gameObject;

            for (int j = 0; j < numberOfBones; j++) {
                if (j == i) { continue; }
                var joint = bone.AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;
                var otherBone = boneContainer.transform.GetChild(j).gameObject;
                joint.connectedBody = otherBone.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
