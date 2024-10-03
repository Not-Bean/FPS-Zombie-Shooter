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
    [SerializeField] GameObject gun;

    public float health;
    public bool dead;
    public int fireCool;
    int shootCool;
    bool canShoot = true;
    bool gunSpin;
    int rotationCount;
    public int ReloadCooldown;
    int reloadCool;
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
        if (reloadCool > 0)
        {
            reloadCool--;
        }


        if (gunSpin)
        {
            gun.transform.Rotate(new Vector3(0, 0, 6));
            rotationCount++;
            if(rotationCount >= 60)
            {
                rotationCount = 0;
                gunSpin = false;
            }
        }
    }

    public void OnShoot()
    {
        if (canShoot && reloadCool <= 0)
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
        
        if (reloadCool <= 0)
        {
            gunSpin = true;
            reloadCool = ReloadCooldown;
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
}
