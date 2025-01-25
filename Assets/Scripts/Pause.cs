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
    [SerializeField] GameObject InventoryScreen;
    PlayerControler playerControl;
    public bool isPaused = false;
    bool inventoryEnabled = false;
    public Button SoundSetting;
    public GameObject VolumeControl;

    private void Start()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();
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
            
            isPaused = false;
            PauseScreen.SetActive(false);
            Time.timeScale = 1;

            UiClose();

            VolumeControl.gameObject.SetActive(false);
        }
        else if (!inventoryEnabled)
        {
            isPaused = true;

            UiOpen();

            PauseScreen.SetActive(true);
            Time.timeScale = 0;

            
        }
        else
        {
            InventoryScreen.SetActive(false);
            Time.timeScale = 1;
            UiClose();
        }
    }

    public void OnInventory()
    {
        if (inventoryEnabled)
        {
            InventoryScreen.SetActive(false);
            Time.timeScale = 1;
            UiClose();
        }
        else if (!isPaused)
        {
            InventoryScreen.SetActive(true);
            Time.timeScale = 0;
            UiOpen();
        }
    }


    void UiOpen()
    {
        playerControl.ShootBlock(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void UiClose()
    {
        playerControl.ShootBlock(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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