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
    public bool isPaused = false;
    public Button SoundSetting;
    public GameObject VolumeControl;

    private void Start()
    {
        //MG = GameObject.FindGameObjectWithTag("Player").GetComponent<ModularGuns>();
        SoundSetting.onClick.AddListener(OnSoundSettings);  
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnPause()
    {
        if (isPaused)
        {
            //MG.ShootBlock(true);
            isPaused = false;
            PauseScreen.SetActive(false);
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            VolumeControl.gameObject.SetActive(false);
        }
        else
        {
            //MG.ShootBlock(false);
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
    
    public void OnSoundSettings()
    {
        VolumeControl.gameObject.SetActive(true);
        PauseScreen.SetActive(false);
    }

    // allows game to be reset to menu from the keyboard
    // usefull for gamecon
    public void OnReset() 
    {
        SceneManager.LoadScene(0);
    }
}