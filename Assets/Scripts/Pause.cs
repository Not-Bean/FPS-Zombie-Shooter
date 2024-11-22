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
    PlayerControler playerControl;
    public bool isPaused = false;

    private void Start()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnPause()
    {
        if (isPaused)
        {
            playerControl.ShootBlock(true);
            isPaused = false;
            PauseScreen.SetActive(false);
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            playerControl.ShootBlock(false);
            isPaused = true;

            PauseScreen.SetActive(true);
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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