using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] GameObject shootPoint;
    [SerializeField] GameObject bullet;

    public float health;
    public bool dead;
    public int fireCool;
    int shootCool;

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

    private void FixedUpdate()
    {
        if (shootCool >= 0)
        {
            shootCool--;
        }
    }

    public void OnShoot()
    {
        if (shootCool <= 0)
        {
            GameObject shot = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
            shot.GetComponent<Rigidbody>().velocity = shootPoint.transform.forward * 60;
            shootCool = fireCool;
        }

    }
}
