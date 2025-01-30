using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareTrain : MonoBehaviour
{
    public GameObject StarterZombie; 
    private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
                StarterZombie.SetActive(true);
            }
        }
}
