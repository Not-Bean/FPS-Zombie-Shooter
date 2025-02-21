using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject SensitivitySlider; 
    public GameObject SettingsButton; 
    public GameObject BackButton;
    public GameObject PlayButton;
    public GameObject QuitButton;
    
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        AudioManager.instance.PauseAllSounds(false);
    }

    public void Options()
    {
        ShowSettingsContent();
        HideMainMenuButtons();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        HideSettingsContent();
        ShowMainMenuButtons();
    }

    public void HideMainMenuButtons()
    {
        SettingsButton.SetActive(false);
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
    }    
    
    public void ShowMainMenuButtons()
    {
        SettingsButton.SetActive(true);
        PlayButton.SetActive(true);
        QuitButton.SetActive(true);
    }

    public void ShowSettingsContent()
    {
        SensitivitySlider.SetActive(true);
        BackButton.SetActive(true);
    }

    public void HideSettingsContent()
    {
        SensitivitySlider.SetActive(false);
        BackButton.SetActive(false);
    }
}
