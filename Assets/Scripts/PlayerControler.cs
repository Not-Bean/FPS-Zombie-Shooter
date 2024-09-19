using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    public float health;
    public bool dead;

    public void Damage(float damage)
    {
        health -= damage;
    }


    private void Update()
    {
        if (health < 0)
        {
            dead = true;
        }

        if (dead)
        {
            Debug.Log("Player Is Dead");
            
            // do dead stuff here
        }
    }
}
