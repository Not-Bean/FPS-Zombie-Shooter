using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectiveTrigger : MonoBehaviour
{
    public bool questCompleted;


    public Objectives os;


    // Start is called before the first frame update
    void Start()
    {
        os = GameObject.FindGameObjectWithTag("Player").GetComponent<Objectives>();
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


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CompleteQuest();
            GiveQuest("Leave The Train Station");
        }
    }
}