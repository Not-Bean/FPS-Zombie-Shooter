using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectiveTrigger : MonoBehaviour
{
    //bool questCompleted;
    public Kills k;
    public Objectives os;
    int goal;
    int mult = 1;

    // Start is called before the first frame update
    void Start()
    {
        goal = k.kills + (50 * mult);
        os = GameObject.FindGameObjectWithTag("Player").GetComponent<Objectives>();
        KillCounter();
    }

    void FixedUpdate()
    {
        KillCounter();
    }

    void GiveQuest(string quest)
    {
        for (int i = 0; i < os.numObjectives; i++)
        {
            os.objectiveQuests[i] = quest;
        }
        
    }


    public void CompleteQuest()
    {
        for (int i = 0; i < os.numObjectives; i++)
        {
            os.objectiveQuests[i] = "";
        }
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