using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    /*
    IEnumerator RandomLoadTime()
    {
        int loadTime = Random.Range(5, 15);
        Debug.Log(loadTime);
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadSceneAsync(1);
    }
    public void PlayGame()
    {
        StartCoroutine(RandomLoadTime());
    }

    Saloon Symphony scene change script

    */
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
