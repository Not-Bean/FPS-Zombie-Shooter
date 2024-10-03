using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float mouseSens;

    private void Awake()
    {
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
