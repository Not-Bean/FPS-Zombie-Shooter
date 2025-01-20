using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowUi : MonoBehaviour
{


    GameObject hoverObj;
    GameObject hoverDrop;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PickUp")
        {
            hoverObj = collision.gameObject;
        }

        if (collision.tag == "DropOff")
        {
            hoverDrop = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PickUp" && collision.gameObject == hoverObj)
        {
            hoverObj = null;
        }

        if (collision.tag == "DropOff" && collision.gameObject == hoverDrop)
        {
            hoverDrop = null;
        }
    }

    public void OnClickPress()
    {

    }
}
