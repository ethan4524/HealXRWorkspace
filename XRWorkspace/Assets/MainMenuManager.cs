using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    float selectionDelay = 1f;
    bool canSelect = true;

    public Image startButton;
    public Image exitButton;
    public Image VRButton;
    public Image ARButton;
    public Image restartButton;
    public Image returnToMenuButton;

    private Color originalColor;

    public GameManager gameManager;
    public GameObject map;
    //public GameObject floor;
    public GameObject centerCamera;
    public GameObject passthrough;

    public GameObject restartButtonObj;
    public GameObject returnToMenuButtonObj;

    public GameObject menuObj;
    public GameObject sceneVR;
    public float maxSpeed = 1;


    void Start()
    {
        // Store the original color
        originalColor = startButton.color;
        EnterVRMode();
        HideGameEndButtons();
        ShowMenu();
    }

    public void ShowMenu() {
        //map.GetComponent<MenuMotion>().moveB = true;
        map.SetActive(false);
       menuObj.SetActive(true);
    }

    public void StartGame()
    {
        if (canSelect) {
            canSelect = false;
            startButton.color = Color.yellow;
            Debug.Log("Start Session!");
            menuObj.SetActive(false);
            
            //map.GetComponent<MenuMotion>().moveA = true;
            map.SetActive(true);
            gameManager.SwitchState(GameManager.State.Load);
            
            Invoke(nameof(ResetCanSelect), selectionDelay);
        }
    }

    public void ExitGame()
    {
        if (canSelect) {
            canSelect = false;
            exitButton.color = Color.yellow;
            Debug.Log("Exiting Game...");
            Invoke(nameof(ResetCanSelect), selectionDelay);
            // Quit the application
            Application.Quit();
        }
    }

    public void EnterVRMode()
    {
        if (canSelect) {
            canSelect = false;
            VRButton.color = Color.yellow;
            Debug.Log("Entering VR mode...");
            Invoke(nameof(ResetCanSelect), selectionDelay);
            // Activate VR mode (replace with your VR activation logic)

            //floor.SetActive(true);
            Camera cam = centerCamera.GetComponent<Camera>();
            RenderSkybox(cam);
            passthrough.SetActive(false);
            sceneVR.SetActive(true);
        }
    }

    public void ShowGameEndButtons() {
        restartButtonObj.SetActive(true);
        returnToMenuButtonObj.SetActive(true);
    }

    public void HideGameEndButtons() {
        restartButtonObj.SetActive(false);
        returnToMenuButtonObj.SetActive(false);
    }

    public void RestartSession() {
        gameManager.Replay();
        HideGameEndButtons();
        StartGame();
    }

    public void ReturnToMenu() {
        gameManager.SwitchState(GameManager.State.Menu);
        HideGameEndButtons();
        ShowMenu();
        

    }

    public void EnterARMode()
    {
        if (canSelect) {
            canSelect = false;
            ARButton.color = Color.yellow;
            Debug.Log("Entering AR mode...");
            Invoke(nameof(ResetCanSelect), selectionDelay);
            // Activate AR mode (replace with your AR activation logic)

            //floor.SetActive(false);
            Camera cam = centerCamera.GetComponent<Camera>();
            RenderColor(Color.black, cam);
            passthrough.SetActive(true);
            sceneVR.SetActive(false);
        }
    }

    void ResetCanSelect()
    {
        startButton.color = originalColor;
        exitButton.color = originalColor;
        VRButton.color = originalColor;
        ARButton.color = originalColor;
        returnToMenuButton.color = originalColor;
        restartButton.color = originalColor;
        canSelect = true;
    }


    public void RenderSkybox(Camera targetCamera = null)
    {
        if (targetCamera == null)
        {
            //Get reference to main camera if no camera is passed
            targetCamera = Camera.main;
        }
        //set camera to render the skybox
        targetCamera.clearFlags = CameraClearFlags.Skybox;
    }

    public void RenderColor(Color color, Camera targetCamera = null)
    {
        if (targetCamera == null)
        {
            //Get reference to main camera if no camera is passed
            targetCamera = Camera.main;
        }

        targetCamera.clearFlags = CameraClearFlags.SolidColor;
        targetCamera.backgroundColor = color;
        var tempColor = color;
        tempColor.a = 0.2f;
        targetCamera.backgroundColor = tempColor;
    }

    

}
