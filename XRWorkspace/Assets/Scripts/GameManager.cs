
using System.Collections;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerGuessData {
    public string answer;
    public string playerGuess;
    public Vector2 animatorState;
    public float guessTime;

    public PlayerGuessData(string _answer, string _playerGuess,Vector2 _animatorState, float _guessTime) {
        answer = _answer;
        playerGuess = _playerGuess;
        animatorState = _animatorState;
        guessTime = _guessTime;
    }
}

public class GameManager : MonoBehaviour
{
    public TextMeshPro textPanel;
    public LeftRightGenerator leftRightGenerator;
    public StareInput stareInput;
    public PlayerData playerData;
    string answer = "";

    private int rounds;
    private int roundCounter = 0;
    private int correctGuesses;
    bool loading = false;

    public MainMenuManager menuManager;

    float currentGuessTime = 0f;
    public enum State {
        Menu,
        Guess,
        Load,
        Finish
    };

    public List<PlayerGuessData> playerGuessData;

    public State gameState;

    private void Start() {
        gameState = State.Menu;
        rounds = 10;
        correctGuesses = 0;
        roundCounter = 0;
        playerGuessData = new List<PlayerGuessData>();
    }

    private void FixedUpdate() {
        
        switch (gameState)
        {
            case State.Menu:
                Debug.Log("The game is in the menu state.");
                
                loading = false;
                roundCounter = 1;
                //playerData.StartTest();
                break;

            case State.Guess:
                Debug.Log("The game is in the game state.");
                currentGuessTime+=Time.deltaTime;
                //Debug.Log(currentGuessTime);
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
                menuManager.ShowGameEndButtons();
                Debug.Log("The game is in the finish state.");
                float score = (float)correctGuesses / (float)rounds;
                score = score * 100;
                string scoreText = score.ToString();
                //playerData.EndTest();
                SetText("Exercise Completed!\nYou scored:" + scoreText + "%");
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
        currentGuessTime = 0f;
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
        PlayerGuessData newData = new PlayerGuessData(answer,_guess,leftRightGenerator.GetAnimatorState(answer), currentGuessTime);
        playerGuessData.Add(newData);
        Debug.Log(newData);
    }

    public void Replay() {
        rounds=10;
        roundCounter=1;
        correctGuesses = 0;
        playerGuessData.Clear();
        loading=false;
        gameState = State.Menu;
    }
    
}
