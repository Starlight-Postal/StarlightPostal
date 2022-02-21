using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Softbodier : MonoBehaviour {

    // These are options that will get applied to all joints
    public float frequency = 2.0F;
    public float dampingRatio = 0.5F;

    [Range(0.0F, 1.0F)]
    public float pressure = 0.5F;

    // Start is called before the first frame update
    void Start() {
        GameObject boneContainer = transform.Find("Bones").gameObject;

        int numberOfBones = boneContainer.transform.childCount;

        // Loop over each bone to attach joints to
        for (int i = 0; i < numberOfBones; i++) {
            var bone = boneContainer.transform.GetChild(i).gameObject;

            GameObject closestOtherBone = null, secondClosestOtherBone = null;
            float closestOtherDist = 999999F;
            float secondClosestOtherDist = 999999F;

            for (int j = 0; j < numberOfBones; j++) {
                var otherBone = boneContainer.transform.GetChild(j).gameObject;
                if (otherBone == bone) { continue; }
                float dist = (bone.transform.position - otherBone.transform.position).sqrMagnitude;
                if (dist < closestOtherDist) {
                    closestOtherBone = otherBone;
                    closestOtherDist = dist;
                }
            }

            for (int j = 0; j < numberOfBones; j++) {
                var otherBone = boneContainer.transform.GetChild(j).gameObject;
                if (otherBone == bone) { continue; }
                if (otherBone == closestOtherBone) { continue; }
                float dist = (bone.transform.position - otherBone.transform.position).sqrMagnitude;
                if (dist < secondClosestOtherDist) {
                    secondClosestOtherBone = otherBone;
                    secondClosestOtherDist = dist;
                }
            }

            GameObject[] nodesToConnect = {closestOtherBone, secondClosestOtherBone, transform.Find("Center").gameObject};

            Debug.Log(bone + " " + nodesToConnect[0] + " " + nodesToConnect[1] + " " + nodesToConnect[2]);

            for (int j = 0; j < 3; j++) {
                if (nodesToConnect[j].GetComponent(typeof(Rigidbody2D)) == null) { continue; } // skip any joints that would be created without a connected body
                var joint = bone.AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;
                joint.connectedBody = nodesToConnect[j].GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
                
                // Apply options from editor
                joint.autoConfigureConnectedAnchor = true;
                joint.autoConfigureDistance = true;
                joint.frequency = frequency;
                joint.dampingRatio = dampingRatio;
                joint.autoConfigureDistance = false;
                joint.autoConfigureConnectedAnchor = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update() {
        var springjoints = GetComponentsInChildren<SpringJoint2D>(); // works recursivly :)

        foreach (var joint in springjoints) {
            var scaledPressure = pressure * (/*out max*/-0.3F - /*out min*/0.0F);

            joint.anchor = (new Vector2() - joint.connectedAnchor) * scaledPressure;
            joint.frequency = frequency;
            joint.dampingRatio = dampingRatio;
        }        
    }
}
