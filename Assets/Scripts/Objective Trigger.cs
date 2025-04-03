using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ObjectiveTrigger : MonoBehaviour
{
    public bool start = false;
    public Kills k;
    public int goal;
    public int mult = 1;
    
    private int currentKills;
    
    public int numCompletions;
    
    public int numObjectives = 1; //number of allowed objectives
    [SerializeField] public TextMeshProUGUI objectiveText;//objectives and objText are the same objects
    [SerializeField] public GameObject objectives;
    public string objectiveQuests; //text strings go here

    // Start is called before the first frame update
    void Start()
    {
        goal = k.kills + (50 * mult); 
        //os = GameObject.FindGameObjectWithTag("Player").GetComponent<Objectives>();
        //KillCounter();
    }

    void FixedUpdate()
    {
        currentKills = k.kills;
        KillCounter();
        objectiveText.text = objectiveQuests;
        //os.GiveQuest("Kill " + os.goal + " Zombies    " + os.k.kills + "/" + os.goal);
        //^this needs to recur a lot
        if (start)
        {
            GiveQuest("Kill " + goal + " Zombies    " + k.kills + "/" + goal);
        }
    }

    public void GiveQuest(string quest)
    {
        objectiveQuests = quest;
    }


    public void CompleteQuest()
    {
        objectiveQuests = "Return To John";
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
            numCompletions++;
            goal *= mult;
        }
        //GiveQuest("Kill " + goal + " Zombies    " + k.kills + "/" + goal);
    }
}