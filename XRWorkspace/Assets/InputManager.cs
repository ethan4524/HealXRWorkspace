using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private bool canInput = true;
    public GameManager gameManager; 
    void Update()
    {
        if (canInput)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(HandleCooldown());
                Debug.Log("Left arrow key pressed.");
                // Call your left arrow action here
                gameManager.CompareGuess("Left");
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(HandleCooldown());
                Debug.Log("Right arrow key pressed.");
                // Call your right arrow action here
                gameManager.CompareGuess("Right");
            }
        }
    }

    IEnumerator HandleCooldown()
    {
        canInput = false;
        yield return new WaitForSeconds(1f); // 1 second cooldown
        canInput = true;
    }
}
