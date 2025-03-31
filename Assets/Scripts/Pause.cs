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
    [SerializeField] GameObject InvScreen;
    [SerializeField] PlayerLook playerLook;
    bool isPauseMenuOpen = false;
    public bool isInventoryOpen = false;
    public Button SoundSetting;
    public GameObject VolumeControl;
    
    public GameObject[] uiController;

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
        if (isPauseMenuOpen)
        {
            PauseScreen.SetActive(false);
            isPauseMenuOpen = false;
            PauseOff();
        }
        else if (isInventoryOpen) 
        {
            InvScreen.SetActive(false);
            isInventoryOpen = false;
            PauseOff();
        }
        else
        {
            PauseScreen.SetActive(true);
            isPauseMenuOpen = true;
            PauseOn();
        }
    }

    public void OnInventory()
    {
        if (isInventoryOpen)
        {
            InvScreen.SetActive(false);
            isInventoryOpen = false;
            PauseOff();
            StartUI();
            //close other ui options
        }
        else if (!isPauseMenuOpen)
        {
            InvScreen.SetActive(true);
            isInventoryOpen = true;
            StopUI();
            PauseOn();
        }
    }

    void PauseOff()
    {
        GetComponent<PlayerControler>().ShootBlock(true);
        AudioManager.instance.PauseAllSounds(false);
        Time.timeScale = 1;
        playerLook.freezeState = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        VolumeControl.gameObject.SetActive(false);
    }
    void PauseOn()
    {
        GetComponent<PlayerControler>().ShootBlock(false);
        AudioManager.instance.PauseAllSounds(true);

        Time.timeScale = 0;
        playerLook.freezeState = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void StopUI()
    {
        for (int i = 0; i < uiController.Length; i++)
        {
            uiController[i].SetActive(false);
        }
    }

    public void StartUI()
    {
        for (int i = 0; i < uiController.Length; i++)
        {
            uiController[i].SetActive(true);
        }
    }
}