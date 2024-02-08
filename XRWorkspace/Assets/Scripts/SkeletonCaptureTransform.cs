using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class SkeletonCaptureTransform : MonoBehaviour
{
    public OVRSkeleton handSkeletonInput;
    public OVRSkeleton handSkeletonOutput;

    public List<OVRBone> myBones = new List<OVRBone>();
    public bool canGetData = true;

    private void Update() {
        GetBoneData();
        if (Input.GetKey(KeyCode.Space)) {
            if (canGetData) {
                canGetData = false;
                GetBoneData();
                Invoke(nameof(ResetCanGetData), 1f);
            }
        }
    }
    private void GetBoneData() {
        
        if (handSkeletonInput == null | handSkeletonOutput == null) {
            Debug.Log("No hand skeleton");
            return;
        }

        for (int i = 0; i < handSkeletonInput.Bones.Count;i++) {
            myBones.Add(handSkeletonInput.Bones[i]);
        }
        
        for (int i = 0; i < handSkeletonOutput.Bones.Count;i++) {
            handSkeletonOutput.Bones[i].Transform = myBones[i].Transform;
        }

        
        
    }

    void ResetCanGetData() {
        canGetData = true;
    }
}
