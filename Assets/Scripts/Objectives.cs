using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ObjectivesScript : MonoBehaviour
{
    [SerializeField]public int numObjectives; //number of allowed objectives
    [SerializeField] public TextMeshProUGUI objectiveText;//objectives and objText are the same objects
    [SerializeField] public GameObject objectives;
    public string[] objectiveQuests; //text strings go here

    //THIS SCRIPT WILL MOST LIKELY USE PICKUP QUESTS
  
    //TODO:
    //assign objectives when talking to NPC's
    //be able to complete objectives
    //be able to remove specific objectives from list



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
            objectives.SetActive(true);
            objectiveText.text = objectiveQuests[i];
            //change later to individualized quests^

        }
    }
}