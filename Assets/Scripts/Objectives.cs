using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Objectives : MonoBehaviour
{
    public int numObjectives; //number of allowed objectives
    [SerializeField] public TextMeshProUGUI[] objectiveText;//objectives and objText are the same objects
    [SerializeField] public GameObject[] objectives;
    public string[] objectiveQuests; //text strings go here


    //THIS SCRIPT WILL MOST LIKELY USE PICKUP QUESTS
  
    //TODO:
    //assign objectives when talking to NPC's
    void Start()
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            objectives[i].SetActive(false);
        }
    }


    void Update()
    {
        UpdateObjectives();
    }


    void UpdateObjectives()
    {
        //get the objectives list to show  proper number of objectives at a time
        //set the objective dialog to objective ui
        for (int i = 0; i <= numObjectives;i++)
        {
            //cycle through the list of objectives and show each active one
            objectives[i].SetActive(true);
            objectiveText[i].text = objectiveQuests[i];
            //change later to individualized quests^
          
            for (int j = 4; j >= numObjectives; j--)
            {
                //cycles through list backwards to remove unused quest slots
                //objectives[j].SetActive(false);
            }
        }
    }
}