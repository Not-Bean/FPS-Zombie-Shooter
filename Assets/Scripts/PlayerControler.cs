using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] GameObject shootPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject Death;

    public float health;
    public bool dead;
    public int fireCool;
    int shootCool;
    bool canShoot = true;

    public int ammoCount;
    public int loadedAmmo;
    public int magSize;

    float maxHealth;


    private void Start()
    {
        maxHealth = health;
    }

    public void Damage(float damage)
    {
        health -= damage;
    }


    private void Update()
    {
        if ( health >= 0)
        {
            healthBar.transform.localScale = new Vector3(10 / maxHealth * health, 1, 5);
        }
        

        if (health < 0)
        {
            dead = true;
        }

        if (dead)
        {
            
            Death.SetActive(true);
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
        if (canShoot)
        {

            if (shootCool <= 0 && loadedAmmo > 0)
            {
                loadedAmmo--;
                GameObject shot = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
                shot.GetComponent<Rigidbody>().velocity = shootPoint.transform.forward * 60;
                shootCool = fireCool;
            }
            else if (loadedAmmo <= 0 && shootCool <= 0)
            {
                OnReload();
            }
        }
    }

    public void ShootBlock(bool value)
    {
        canShoot = value;
    }

    public void OnReload()
    {
        if (ammoCount < magSize - loadedAmmo)
        {
            loadedAmmo = ammoCount;
            ammoCount = 0;
        }
        else
        {
            ammoCount -= magSize - loadedAmmo;
            loadedAmmo = magSize;
        }
    }
}
