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
        Debug.Log("COLISSION");
        if (collision.tag == "InventoryItem")
        {
            hoverObj = collision.gameObject;
            Debug.Log("ITEM_HOV");
        }

        if (collision.tag == "DropOff")
        {
            hoverDrop = collision.gameObject;
            Debug.Log("DROP_HOV");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "InventoryItem" && collision.gameObject == hoverObj)
        {
            hoverObj = null;
            Debug.Log("ITEM_CLEAR_HOV");
        }

        if (collision.tag == "DropOff" && collision.gameObject == hoverDrop)
        {
            hoverDrop = null;
            Debug.Log("DROP_CLEAR_HOV");
        }
    }

    public void OnClickPress()
    {
        Debug.Log("CLICK");
        if (hoverObj != null)
        {
            Debug.Log("VALID");
            hoverObj.transform.position = transform.position;
        }
    }

    public void OnClickRelease()
    {
        if (hoverDrop != null)
        {

        }
        else
        {

        }
    }
}
