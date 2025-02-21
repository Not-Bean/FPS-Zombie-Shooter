using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {
    float sensx;
    float sensy;

    public Transform orientation;

    float xrotation;
    float yrotation;

    public bool freezeState = false;
    float freeze = 0;
    private void Start()
    {
        try
        {
            var sens = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SensitivityGet();

            sensx = sens * 10 + 5;
            sensy = sens * 10 + 5;
        }
        catch 
        {
            Debug.LogWarning("ERROR NO GAME MANAGER FOUND (Expected when game not started from the main menu SAFE TO IGNORE)");
            sensx = 0.5f * 10 + 5;
            sensy = 0.5f * 10 + 5;
        }


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        float mousex = 0;
        float mousey = 0;
        if (!freezeState)
        {
            mousex = Input.GetAxisRaw("Mouse X") * sensx;
            mousey = Input.GetAxisRaw("Mouse Y") * sensy;
        }
        
        yrotation += mousex;
        xrotation -= mousey;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xrotation, yrotation, 0);
        orientation.rotation = Quaternion.Euler(0, yrotation, 0);
    }
}

