/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceController : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public GameManager gameManager;

    private void Start() {
        actions.Add("Left",Left);
        actions.Add("Right",Right);
        actions.Add("Yes",Yes);
        actions.Add("No",No);
    
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    
    } 

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {
        Debug.Log(speech.text);
        if (gameManager.gameState == GameManager.State.Guess) {
            actions[speech.text].Invoke();
        }
        
    }

    public void Left() {
        gameManager.CompareGuess("Left");
    }

    public void Right() {
        gameManager.CompareGuess("Right");
    }

    public void Yes() {
        gameManager.Replay();
    }

    public void No() {
        Application.Quit();
    }

    
}
*/