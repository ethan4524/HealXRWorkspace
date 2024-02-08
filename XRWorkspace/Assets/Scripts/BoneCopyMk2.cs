using UnityEngine;

public class BoneCopyMk2 : MonoBehaviour
{
    public SkinnedMeshRenderer sourceSkinnedMeshRenderer;
    
    public SkinnedMeshRenderer targetSkinnedMeshRenderer;
    public GameObject source;
    public GameObject target;
    public Transform playerHead;

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

        // Get the position of the player's head in world space
        Vector3 headPosition = playerHead.position;

        // Copy bone transforms
        for (int i = 0; i < sourceSkinnedMeshRenderer.bones.Length; i++)
        {
            Transform sourceBone = sourceSkinnedMeshRenderer.bones[i];
            Transform targetBone = targetSkinnedMeshRenderer.bones[i];

            targetBone.localScale = sourceBone.localScale;
            
            
            // Mirror position across the player's head
            Vector3 mirroredPosition = MirrorPosition(sourceBone.position, headPosition);
            targetBone.position = mirroredPosition;

            Vector3 sourceRotation = sourceBone.localRotation.eulerAngles;
            Vector3 mirroredRotation = new Vector3(-sourceRotation.x, sourceRotation.y, sourceRotation.z);
            targetBone.localRotation = Quaternion.Euler(mirroredRotation);
            
           
            

        }

    }

    

    // Mirror position across a given mirror axis (player's head in this case)
    Vector3 MirrorPosition(Vector3 position, Vector3 mirrorAxis)
    {
        Vector3 mirroredPosition = position;
        mirroredPosition.x = 2 * mirrorAxis.x - position.x;
        return mirroredPosition;
    }

    // Mirror rotation by flipping the sign of the X component of the rotation Euler angles
    Quaternion MirrorRotation(Quaternion rotation)
    {
        Vector3 eulerAngles = rotation.eulerAngles;
        eulerAngles.x = -eulerAngles.x;
        return Quaternion.Euler(eulerAngles);
    }
}
