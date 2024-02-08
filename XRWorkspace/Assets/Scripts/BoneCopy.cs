using System;
using UnityEngine;

public class BoneCopy : MonoBehaviour
{
    public SkinnedMeshRenderer sourceSkinnedMeshRenderer;
    public SkinnedMeshRenderer targetSkinnedMeshRenderer;

    

    public Vector3 offset;

    void Update()
    {
        CopyBoneTransformsFromSource();
    }

    void CopyBoneTransformsFromSource()
    {
        // Check if source and target SkinnedMeshRenderers are assigned
        if (sourceSkinnedMeshRenderer == null || targetSkinnedMeshRenderer == null)
        {
            Debug.LogError("Source or target SkinnedMeshRenderer not assigned.");
            return;
        }

        // Check if the bone arrays have the same length
        if (sourceSkinnedMeshRenderer.bones.Length != targetSkinnedMeshRenderer.bones.Length)
        {
            Debug.LogError("Source and target SkinnedMeshRenderers have different bone structures.");
            return;
        }

        // Copy bone transforms
        for (int i = 0; i < sourceSkinnedMeshRenderer.bones.Length; i++)
        {
            Transform sourceBone = sourceSkinnedMeshRenderer.bones[i];
            Transform targetBone = targetSkinnedMeshRenderer.bones[i];

            // Copy position and scale directly
            targetBone.localPosition = sourceBone.localPosition *-1;
            targetBone.localScale = sourceBone.localScale;

            

            // For rotation, mirror along the X axis (assuming X is the axis pointing from right to left)
            // You may need to adjust this based on your specific model orientation
            Vector3 sourceRotation = sourceBone.localRotation.eulerAngles;
            Vector3 mirroredRotation = new Vector3(-sourceRotation.x, sourceRotation.y, sourceRotation.z);
            targetBone.localRotation = Quaternion.Euler(mirroredRotation);
        }
        
    }
}
