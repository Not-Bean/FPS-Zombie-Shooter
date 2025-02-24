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
    bool isInventoryOpen = false;
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
        else if (!inventoryEnabled)
        {
            //MG.ShootBlock(false);
            isPaused = true;
            AudioManager.instance.PauseAllSounds(true);

            UiOpen();

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
        }
        else if (!isPauseMenuOpen)
        {
            InvScreen.SetActive(true);
            isInventoryOpen = true;
            PauseOn();
        }
    }

    void PauseOff()
    {
        //its backwards for some reason
        //MG.ShootBlock(true);
        AudioManager.instance.PauseAllSounds(false);
        Time.timeScale = 1;
        playerLook.freezeState = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        VolumeControl.gameObject.SetActive(false);
    }
    void PauseOn()
    {
        //MG.ShootBlock(false);
        AudioManager.instance.PauseAllSounds(true);

        Time.timeScale = 0;
        playerLook.freezeState = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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