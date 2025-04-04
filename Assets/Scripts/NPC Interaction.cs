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


   [SerializeField] bool questNPC;
   public bool questCompleted;
   public ObjectiveTrigger os;
   [SerializeField] string newQuest;
  
   //MOST FOR LOOPS ARE REDUNDANT BUT WILL STAY UNTIL I FIGURE OUT HOW TO WORK 5 OBJECTIVES AT ONCE
  
   void Start()
   {
       npcNameText.text = npcName;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();

        ui.SetActive(false);
        uiPanel.SetActive(false);
        findLook = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerLook>();
        //os = GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectiveTrigger>();
   }
   void Update()
   {
       questCompleted = false;
       if (os.k.kills >= os.goal)
       {
           os.start = false;
           questCompleted = true;
       }
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
   private void OnTriggerStay(Collider player)
   {
       if (player.CompareTag("Player"))
       {
           inRange = true;
           ui.SetActive(true);
       }


       if (dialogActive)
       {
           ui.SetActive(false);
       }
   }


   private void OnTriggerExit(Collider player)
   {
       inRange = false;
       ui.SetActive(false);
   }

   void CheckQuest()
   {
       if (os.numCompletions >= 1)
       {
           //give the player additional ammo for completing the quest
           MG.ammoCount += MG.magSize - MG.loadedAmmo + 150;
           npcDialog[0] = "Thanks for helping me! Your reward is some of my stockpiled ammo";
           npcDialog[1] = "I Still need time to build the boat. Keep me covered while I keep building it";
           npcDialog[2] = "Once I'm done you can join me in the boat.";
       }

       if (os.numCompletions >= 3)
       {
           npcDialog[0] = "I've finished building the boat";
           npcDialog[1] = "It's finally time to leave this stupid island";
           npcDialog[2] = "Come to the dock, we'll get in the boat there.";
           os.objectiveQuests = "Leave The Island";
       }
   }

   IEnumerator PlayDialog(){
       CheckQuest();
       for (int i = 0; i < npcDialog.Length; i++)//iterates through the for loop multiple times for some reason
       {
           Pause();
           uiText.SetText(npcDialog[i]);
           AudioManager.instance.PlayOneShot(FMODEvents.instance.ScriptNPC,this.transform.position);
           yield return new WaitForSecondsRealtime(5f);
           //yield return new WaitForSeconds(5f);
           if (i >= npcDialog.Length - 1)//unfreeze
           {
               dialogActive = false;
               uiPanel.SetActive(false);
               npcNameText.text = npcName;
               playerController.ShootBlock(true);
               AudioManager.instance.PauseAllSounds(false);
               //p.isPaused = true;
               Time.timeScale = 1;
               //Cursor.lockState = CursorLockMode.Locked;
               findLook.freezeState = false;
               //Cursor.visible = false;
           }
       }

       os.start = true;
       if (questCompleted && os.numCompletions < 3)
       {
          os.GiveQuest("Kill " + os.goal + " Zombies    " + os.k.kills + "/" + os.goal);
       }
   }


   void Pause()
   {
       AudioManager.instance.PauseAllSounds(true);
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
