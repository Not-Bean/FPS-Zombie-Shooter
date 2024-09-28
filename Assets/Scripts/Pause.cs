using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject PauseScreen;

    bool isPaused = false;

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnPause()
    {
        if (isPaused)
        {
            isPaused = false;
            PauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;

            PauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Sensitivity(float value)
    {
        Debug.Log(value);
    }

    // allows game to be reset to menu from the keyboard
    // usefull for gamecon
    public void OnReset() 
    {
        SceneManager.LoadScene(0);
    }
}