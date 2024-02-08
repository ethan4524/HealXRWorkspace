using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LeftRightGenerator : MonoBehaviour
{
    public GameObject leftHandModel;
    public GameObject rightHandModel;
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    private void Start() {
        HideHands();
    }
    public string GenerateRandomDirection() {
        int r = UnityEngine.Random.Range(0,2);
        // Generate a random rotation around the x-axis
        float randomXRotation = UnityEngine.Random.Range(0f, 360f);
        // Apply the rotation
    
        if (r==0) {
            //left
            leftHandModel.SetActive(true);
            leftHandAnimator.SetFloat("LeftHandBlendX", UnityEngine.Random.Range(-1f, 1f));
            leftHandAnimator.SetFloat("LeftHandBlendY", UnityEngine.Random.Range(-1f, 1f));
            
            leftHandModel.transform.rotation = Quaternion.Euler(randomXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            return "Left";
        } else {
            //right
            rightHandModel.SetActive(true);
            rightHandAnimator.SetFloat("RightHandBlendX", UnityEngine.Random.Range(-1f,1f));
            rightHandAnimator.SetFloat("RightHandBlendY", UnityEngine.Random.Range(-1f,1f));
            
            rightHandModel.transform.rotation = Quaternion.Euler(randomXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            return "Right";
        }
    }

    public void HideHands() {
        leftHandModel.SetActive(false);
        rightHandModel.SetActive(false);
    }
}
