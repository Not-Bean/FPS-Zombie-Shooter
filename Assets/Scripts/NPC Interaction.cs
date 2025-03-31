using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NPCInteraction : MonoBehaviour
{
   PlayerControler playerController;

   public bool inRange;
   public bool dialogActive;
   public GameObject ui;
   public GameObject uiPanel;
  
   [SerializeField] string npcName;
   [SerializeField] string[] npcDialog;


   public TextMeshProUGUI uiText;
   public TextMeshProUGUI npcNameText;


   public Pause p;
   public ModularGuns MG;
   public PlayerLook findLook;


   public bool questNPC;
   public bool questCompleted;
   public ObjectiveTrigger os;
   [SerializeField] string newQuest;
  
   //MOST FOR LOOPS ARE REDUNDANT BUT WILL STAY UNTIL I FIGURE OUT HOW TO WORK 5 OBJECTIVES AT ONCE
  
   void Start()
   {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();

        ui.SetActive(false);
        uiPanel.SetActive(false);
        findLook = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerLook>();
        //os = GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectiveTrigger>();
   }


   void Update()
   {
       if (inRange && Input.GetKeyDown(KeyCode.F))
       {
           dialogActive = true;
       }


       if (dialogActive)
       {
           uiPanel.SetActive(true);
           StartCoroutine(PlayDialog());
           //essentially pause the game
           //display the dialog
       }
       //CompleteQuest();
   }
   private void OnTriggerStay(Collider Player)
   {
       if (Player.CompareTag("Player"))
       {
           inRange = true;
           ui.SetActive(true);
       }


       if (dialogActive)
       {
           ui.SetActive(false);
       }
   }


   private void OnTriggerExit(Collider Player)
   {
       inRange = false;
       ui.SetActive(false);
   }


   IEnumerator PlayDialog(){
       for (int i = 0; i < npcDialog.Length; i++)//iterates through the for loop multiple times for some reason
       {
           Pause();
           uiText.SetText(npcDialog[i]);
           yield return new WaitForSecondsRealtime(5f);
           //yield return new WaitForSeconds(5f);
           if (i >= npcDialog.Length - 1)//unfreeze
           {
               
               dialogActive = false;
               uiPanel.SetActive(false);
               npcNameText.text = npcName;
                playerController.ShootBlock(true);
               //p.isPaused = true;
               Time.timeScale = 1;
               //Cursor.lockState = CursorLockMode.Locked;
               findLook.freezeState = false;
               //Cursor.visible = false;
           }
       }
       CheckQuest();
   }


   void CheckQuest()
   {
       if (os.numCompletions >= 1)
       {
           //give the player additional ammo for completing the quest
           MG.ammoCount += MG.magSize - MG.loadedAmmo + 150;
           os.numCompletions--;
       }
   }

/*
   public void CompleteQuest()//TO ACTIVATE, MAKE "questCompleted" = true
   {
       for (int i = 0; i <= os.numObjectives;i++)
       {
           if (questCompleted)
           {
               os.objectiveQuests = "";
               questCompleted = false;
              
               //
               //ADD PAYOUT HERE
               //
           }
       }
   }
*/

   void Pause()
   {
       playerController.ShootBlock(false);
       //p.isPaused = false;
       Time.timeScale = 0;
       //Cursor.lockState = CursorLockMode.None;
      
       findLook.freezeState = true;
       //Cursor.visible = true;
       dialogActive = false;
       //uiPanel.SetActive(false);
       StopCoroutine(PlayDialog());
   }
}
