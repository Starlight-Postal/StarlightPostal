using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Creates the joints required for simulating soft-body physics on a rigged sprite
 *
 * To use:
 * create a sprite and rig it with bones
 * add the "Sprite Skin" component and click "Create Bones"
 * create empty child object called "Bones"
 * move all the bones to under "Bones"
 * select all bones and apply Rigidbody2D, and a collider of your choise and tweak
 */
public class SoftBodyifier : MonoBehaviour {

    // These are options that will get applied to all joints
    public float frequency = 2.0F;
    public float dampingRatio = 0.5F;

    /**
     * Connects every bone to every other bone using spring joints
     * This can be done manually but it is absolutely painful :)
     */
    void Start() {
        GameObject boneContainer = transform.Find("Bones").gameObject;

        int numberOfBones = boneContainer.transform.childCount;

        // Loop over each bone to attach joints to
        for (int i = 0; i < numberOfBones; i++) {
            var bone = boneContainer.transform.GetChild(i).gameObject;

            // Loop over each other bone to attach to the i'th bone
            for (int j = 0; j < numberOfBones; j++) {
                if (j == i) { continue; }
                var joint = bone.AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;
                var otherBone = boneContainer.transform.GetChild(j).gameObject;
                joint.connectedBody = otherBone.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;

                // Apply options from editor
                joint.autoConfigureConnectedAnchor = true;
                joint.frequency = frequency;
                joint.dampingRatio = dampingRatio;
            }
        }
    }
}
