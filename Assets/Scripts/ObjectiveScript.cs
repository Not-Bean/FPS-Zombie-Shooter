using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = Unity.Mathematics.Random;


public class ObjectivesScript : MonoBehaviour
{
    public int numObjectives; //number of allowed objectives
    [SerializeField] public TextMeshProUGUI objectiveText;//objectives and objText are the same objects
    public string[] objectiveQuests; //text strings go here
    
    //THIS SCRIPT WILL MOST LIKELY USE PICKUP QUESTS



    void Update()
    {
        for (int i = 0; i < numObjectives;i++)
        {
            //cycle through the list of objectives and show each active one
            objectiveText.text = objectiveQuests[i];
        }
//this is a comment
    }
    
}