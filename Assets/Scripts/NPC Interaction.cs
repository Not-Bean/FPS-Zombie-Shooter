using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public bool inRange;
    public bool dialogActive;
    public GameObject ui;
    
    public string npcName;
    public string[] npcDialog;

    void Start()
    {
        ui.SetActive(false);
    }

    void Update()
    {
        if (inRange)
        {
            ui.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider Player)
    {
        inRange = true;
        ui.SetActive(true);
    }

    private void OnTriggerExit(Collider Player)
    {
        inRange = false;
        ui.SetActive(false);
    }
}
