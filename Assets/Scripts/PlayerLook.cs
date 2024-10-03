using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {
    float sensx;
    float sensy;

    public Transform orientation;

    float xrotation;
    float yrotation;

    private void Start()
    {
        try
        {
            var sens = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SensitivityGet();

            sensx = sens * 1000 + 500;
            sensy = sens * 1000 + 500;
        }
        catch 
        {
            Debug.LogWarning("ERROR NO GAME MANAGER FOUND (Expected when game not started from the main menu SAFE TO INGROE)");
            sensx = 0.5f * 1000 + 500;
            sensy = 0.5f * 1000 + 500;
        }


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        float mousex = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensx;
        float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensy;

        yrotation += mousex;
        xrotation -= mousey;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xrotation, yrotation, 0);
        orientation.rotation = Quaternion.Euler(0, yrotation, 0);
    }
}

