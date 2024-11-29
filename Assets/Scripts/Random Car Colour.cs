using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCarColour : MonoBehaviour
{
    public Material[] carColours;
    public GameObject[] cars;
    
    // Start is called before the first frame update
    void Start()
    {
        int randomColour = Random.Range(0, carColours.Length);
        
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].GetComponent<Renderer>().material = carColours[randomColour];
        }
    }
}
