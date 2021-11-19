using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//In our case it should not be dontdestroyable. 
public class GameManager : MonoBehaviour
{

    public Canvas gameplayCanvas;
    public Canvas winfailCanvas;

    public Text winFailText;

    [TextArea]
    public string winText = "You won!\nYou are a natural pig!";
    [TextArea]
    public string failText = "You lose!\nYou looks so pathetic!\nNow you are going to become a pork..";

    // Start is called before the first frame update
    void Start()
    {
        StartTheGame();
    }

    public void StartTheGame() {
        switchGameMode(true);
    }

    public void StopTheGame(bool win) {
        winFailText.text = win ? winText : failText;
        switchGameMode(false);
    }

    public void RestartTheGame() {
        //To make everything simple and to avoid resetting and respawning everything lets just reload the scene
        SceneManager.LoadScene(0); //We have just one scene so... It's 0
    }

    private void switchGameMode(bool gameplay) {
        gameplayCanvas.enabled = gameplay;
        winfailCanvas.enabled = !gameplay;
        Time.timeScale = gameplay?1:0;
    }

    
}
