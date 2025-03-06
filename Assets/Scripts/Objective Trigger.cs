using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public bool questCompleted;
    public ObjectivesScript os;
    void Start()
    {
        os = GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectivesScript>();
    }
    void GiveQuest(string newQuest)
    {
        for (int i = 0; i <= os.numObjectives; i++)
        {
            questCompleted = false;
            os.objectiveQuests[i] = newQuest;
        }
    }
    
    public void CompleteQuest()//TO ACTIVATE, MAKE "questCompleted" = true
    {
        for (int i = 0; i <= os.numObjectives;i++)
        {
            if (questCompleted)
            {
                os.objectiveQuests[i] = "";
                questCompleted = false;
              
                //
                //ADD PAYOUT HERE
                //
            }
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
