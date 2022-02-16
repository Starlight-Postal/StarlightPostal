using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureVesselBody : SoftBodyifier {

    public float pressure = 1.0F;

    private GameObject boneContainer;
    private int numberOfBones;

    void Start() {
        base.Start();
        var springjoints = GetComponentsInChildren<SpringJoint2D>(); // works recursivly :)

        foreach (var joint in springjoints) {
            joint.autoConfigureDistance = false;
                joint.autoConfigureConnectedAnchor = false;
        }
    }

    // Update is called once per frame
    void Update() {
        base.Update();
        var springjoints = GetComponentsInChildren<SpringJoint2D>(); // works recursivly :)

        foreach (var joint in springjoints) {
            joint.anchor = (new Vector2() - joint.connectedAnchor) * pressure;
        }
    }
}

