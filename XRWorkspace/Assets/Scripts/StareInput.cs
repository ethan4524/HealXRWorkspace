using UnityEngine;

public class StareInput : MonoBehaviour
{
    public float maxStareDuration = 0.5f; // Maximum duration for staring in seconds
    public LayerMask objectLayer; // Layer containing the objects to detect
    public GameObject leftStareSensor;
    public GameObject rightStareSensor;
    public StareThrobber leftLoadingText;
    public StareThrobber rightLoadingText;
    public GameObject playerHead;
    public GameManager gameManager;

    private float leftStareTimer = 0f;
    private float rightStareTimer = 0f;
    private bool isLeftStaring = false;
    private bool isRightStaring = false;

    public bool canStare = false;

    void Update()
    {
        // Update stare timer and fill amount for both left and right stare sensors
        if (canStare & gameManager.gameState != GameManager.State.Menu) {
            UpdateStare(leftStareSensor, ref leftStareTimer, leftLoadingText, ref isLeftStaring);
            UpdateStare(rightStareSensor, ref rightStareTimer, rightLoadingText, ref isRightStaring);
        }
        
    }

    void UpdateStare(GameObject stareSensor, ref float stareTimer, StareThrobber loadingText, ref bool isStaring)
    {
        if (stareSensor == null || loadingText == null)
            return;

        // Cast a ray from the stare sensor
        Ray ray = new Ray(playerHead.transform.position, playerHead.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.blue);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, objectLayer))
        {
            // Check if the detected object is one of the options
            if (hit.collider.gameObject == stareSensor)
            {
                // Start or continue staring
                isStaring = true;
                stareTimer += Time.deltaTime;

                // Calculate fill amount based on stare duration
                float fillAmount = Mathf.Clamp01(stareTimer / maxStareDuration);
                // Update fill amount
                loadingText.SetFillAmount(fillAmount);

                // If staring duration exceeds the maximum, call the select method
                if (stareTimer >= maxStareDuration)
                {
                    SelectOption(stareSensor);
                    // Reset stare timer and fill amount
                    ResetStare(ref stareTimer, loadingText, ref isStaring);
                }
            }
            else
            {
                // Reset stare timer and fill amount if the detected object is not the stare sensor
                ResetStare(ref stareTimer, loadingText, ref isStaring);
            }
        }
        else
        {
            // Reset stare timer and fill amount if no object is detected
            ResetStare(ref stareTimer, loadingText, ref isStaring);
        }
    }

    void ResetStare(ref float stareTimer, StareThrobber loadingText, ref bool isStaring)
    {
        isStaring = false;
        stareTimer = 0f;
        // Reset fill amount
        loadingText.SetFillAmount(0f);
    }

    void SelectOption(GameObject stareSensor)
    {
        if (stareSensor.name.ToString().Equals("LeftStare")) {
            Debug.Log("Chose Left!");
            gameManager.CompareGuess("Left");
        } else {
            Debug.Log("Chose Right");
            gameManager.CompareGuess("Right");
        }
        

        
        canStare = false;
    }
}
