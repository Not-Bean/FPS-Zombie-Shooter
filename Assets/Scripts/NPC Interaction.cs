using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NPCInteraction : MonoBehaviour
{
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
   public Objectives os;
   [SerializeField] string newQuest;
  
   //MOST FOR LOOPS ARE REDUNDANT BUT WILL STAY UNTIL I FIGURE OUT HOW TO WORK 5 OBJECTIVES AT ONCE
  
   void Start()
   {
       ui.SetActive(false);
       uiPanel.SetActive(false);
       findLook = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerLook>();
       os = GameObject.FindGameObjectWithTag("Player").GetComponent<Objectives>();
   }


   void Update()
   {
       if (inRange && Input.GetKeyDown(KeyCode.E))
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
       CompleteQuest();
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
               GiveQuest();
               print("Quest Given");
               dialogActive = false;
               uiPanel.SetActive(false);
               npcNameText.text = npcName;
               MG.ShootBlock(true);
               //p.isPaused = true;
               Time.timeScale = 1;
               //Cursor.lockState = CursorLockMode.Locked;
               findLook.freezeState = false;
               //Cursor.visible = false;
           }
       }
   }


   void GiveQuest()
   {
       for (int i = 0; i <= os.numObjectives; i++)
       {
           if (questNPC)
           {
               questCompleted = false;
               os.objectiveQuests[i] = newQuest;
           }


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


   void Pause()
   {
       MG.ShootBlock(false);
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
