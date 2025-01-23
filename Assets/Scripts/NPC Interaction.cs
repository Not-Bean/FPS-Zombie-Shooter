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
    void Start()
    {
        ui.SetActive(false);
        uiPanel.SetActive(false);
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
            //Fix Bug: dialog boxes overlap and glitch out
        }
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

    IEnumerator PlayDialog()
        
        for (int i = 0; i < npcDialog.Length; i++)//iterates through the for loop multiple times for some reason
        {
            Pause();
            uiText.SetText(npcDialog[i]);
            yield return new WaitForSeconds(5f);
            if (i >= npcDialog.Length - 1)
            {
                dialogActive = false;
                npcNameText.text = npcName;
                //Pause doesn't properly pause the game
                MG.ShootBlock(true);
                p.isPaused = true;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    void Pause()
    {
        MG.ShootBlock(false);
        p.isPaused = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dialogActive = false;
        //uiPanel.SetActive(false);
        StopCoroutine(PlayDialog());
    }
}
