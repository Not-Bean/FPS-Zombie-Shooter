using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ObjectiveTrigger : MonoBehaviour
{
    //bool questCompleted;
    public Kills k;
    int goal;
    int mult = 1;
    
    public int numObjectives; //number of allowed objectives
    [SerializeField] public TextMeshProUGUI objectiveText;//objectives and objText are the same objects
    [SerializeField] public GameObject objectives;
    public string objectiveQuests; //text strings go here

    // Start is called before the first frame update
    void Start()
    {
        goal = k.kills + (50 * mult); 
        //os = GameObject.FindGameObjectWithTag("Player").GetComponent<Objectives>();
        KillCounter();
    }

    void FixedUpdate()
    {
        KillCounter();
        objectiveText.text = objectiveQuests;
    }

    void GiveQuest(string quest)
    {
        for (int i = 0; i < numObjectives; i++)
        { 
            objectiveQuests = quest;
        }
        
    }


    public void CompleteQuest()
    {
        objectiveQuests = "";
    }

/*
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CompleteQuest();
            GiveQuest("Leave The Train Station");
        }
    }
    RETIRED FUNCTION. USED ON OLD MAP
*/
    void KillCounter()
    {
        if (k.kills >= goal)
        {
            CompleteQuest();
            mult++;
            goal *= mult;
        }
        GiveQuest("Kill " + goal + " Zombies    " + k.kills + "/" + goal);
    }
}