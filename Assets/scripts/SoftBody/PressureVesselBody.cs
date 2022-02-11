using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureVesselBody : SoftBodyifier {

    public float pressure = 0.0F;

    private GameObject boneContainer;
    private int numberOfBones;

    void Start() {
        base.Start();
    }

    // Update is called once per frame
    void Update() {
        base.Update();
        Vector3 objPos = this.transform.position;
        for (int i = 0; i < numberOfBones; i++) {
            var bone = boneContainer.transform.GetChild(i).gameObject;
            Vector3 bonePos = bone.transform.position;
            Vector3 direction = objPos - bonePos;

            var rb = bone.gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
            rb.AddForce(Vector3.up * pressure);
        }
    }
}
