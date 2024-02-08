
using System.Collections;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class GameManager : MonoBehaviour
{
    public TextMeshPro textPanel;
    public LeftRightGenerator leftRightGenerator;
    public StareInput stareInput;
    string answer = "";

    private int rounds;
    private int roundCounter = 0;
    private int correctGuesses;
    bool loading = false;
    public enum State {
        Menu,
        Guess,
        Load,
        Finish
    };

    public State gameState;

    private void Start() {
        gameState = State.Menu;
        rounds = 10;
        correctGuesses = 0;
        roundCounter = 0;
    }

    private void FixedUpdate() {
        
        switch (gameState)
        {
            case State.Menu:
                Debug.Log("The game is in the menu state.");
                SwitchState(State.Load);
                loading = false;
                roundCounter = 1;
                break;

            case State.Guess:
                Debug.Log("The game is in the game state.");

                break;

            case State.Load:
                Debug.Log("The game is in the load state.");
                if (loading) {
                    return;
                }
                if (roundCounter <= rounds) {
                    SetText("");
                    loading = true;
                    StartCoroutine(StartCountdown());
                } else {
                    gameState = State.Finish;
                }
                
                break;

            case State.Finish:
                Debug.Log("The game is in the finish state.");
                float score = (float)correctGuesses / (float)rounds;
                score = score * 100;
                string scoreText = score.ToString();
                SetText("Exercise Completed!\nYou scored a " + scoreText + 
                    "%\nTo play again, say 'yes' or 'no'");
                break;

            default:
                Debug.Log("Unknown state.");
                break;
        }

    }

    public void SwitchState(State newState) {
        gameState = newState;
    }

    public void SetText(string _text) {
        textPanel.text = _text;
    }

    string GenerateRandomDirection()
    {
        int randomIndex = Random.Range(0, 2);

        string randomDirection = (randomIndex == 0) ? "Left" : "Right";

        return randomDirection;
    }

    IEnumerator StartCountdown()
    {
        
        SetText("3");
        yield return new WaitForSeconds(1f);
        SetText("2");
        yield return new WaitForSeconds(1f);
        SetText("1");
        yield return new WaitForSeconds(1f);
        Debug.Log("Countdown complete!");
        answer = leftRightGenerator.GenerateRandomDirection();
        SetText("Guess!");
        gameState = State.Guess;
        stareInput.canStare = true;
    }

    public void CompareGuess(string _guess) {
        stareInput.canStare = false;
        if (_guess.Equals(answer)) {
            Debug.Log("Correct Guess");
            correctGuesses+=1;
        } else {
            Debug.Log("Incorrect Guess");
        }
        roundCounter+=1;
        gameState = State.Load;
        loading = false;
        leftRightGenerator.HideHands();
    }

    public void Replay() {
        rounds=10;
        roundCounter=1;
        correctGuesses = 0;
        loading=false;
        gameState = State.Menu;
    }
    
}
