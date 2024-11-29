using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float mouseSens;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    public void SensitivitySet(float value)
    {
        mouseSens = value;
    }

    public float SensitivityGet()
    {
        return mouseSens;
    }
}
