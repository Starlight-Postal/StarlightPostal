using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureVesselBody : SoftBodyifier {

    [Range(0.0F, 1.0F)]
    public float pressure = 0.5F;

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
            var scaledPressure = pressure * (/*out max*/-0.3F - /*out min*/0.0F);

            joint.anchor = (new Vector2() - joint.connectedAnchor) * scaledPressure;
        }
    }
}

