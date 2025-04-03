using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollider : MonoBehaviour
{
    public int size;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject gun = GameObject.FindGameObjectWithTag("Gun");
            gun.GetComponent<ModularGuns>().ammoCount += size;
            Destroy(transform.parent.gameObject);
        }
    }

}
