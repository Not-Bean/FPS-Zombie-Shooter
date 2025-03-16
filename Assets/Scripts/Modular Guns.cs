using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ModularGuns : MonoBehaviour
{
    [SerializeField] PlayerControler playerControler;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gunModel;
    [SerializeField] GameObject shootPoint;
    [SerializeField] TextMeshProUGUI totalAmmoText;
    [SerializeField] TextMeshProUGUI loadedAmmoText;
    [SerializeField] int fireCool;
    int shootCool;
    [SerializeField] int magSize;
    [SerializeField] int loadedAmmo;
    [SerializeField] int ammoCount;
    int reloadCool;
    int rotationCount;
    [SerializeField] int ReloadCooldown;
    public bool canShoot = true;
    public bool canShootExt = true;
    bool gunSpin;
    [SerializeField] Image ammoCircle;
    [SerializeField] Image redCircle;
    [SerializeField] public int damage;
    
    [SerializeField] bool isFullAuto;
    [SerializeField] float fullAutoCooldown;
    public ParticleSystem muzzleFlash;
    
    public float range = 100f;
    
    int autoCool = -1;
    

    void Start()
    {
        totalAmmoText.text = ammoCount.ToString();
        loadedAmmoText.text = loadedAmmo.ToString();
        SetAmmo();
    }



    private void FixedUpdate()
    {
        loadedAmmoText.text = loadedAmmo.ToString();
        totalAmmoText.text = ammoCount.ToString();
        SetAmmo();

        

        if (shootCool >= 0)
        {
            shootCool--;
        }

        if (reloadCool > 0)
        {
            reloadCool--;
        }

        if (autoCool >= 0)
        {
            autoCool++;
            if (autoCool % fullAutoCooldown == 0)
            {
                OnShoot();
            }
        }

        if (gunSpin)
        {
            gunModel.transform.Rotate(new Vector3(0, 0, 6));
            rotationCount++;
            if (rotationCount >= 60)
            {
                rotationCount = 0;
                gunSpin = false;
                canShoot = true;
            }
        }
    }

    public void OnClickPress()
    {
        if (isFullAuto)
        {
            autoCool = 0;
        }
    }

    public void OnClickRelease()
    {
        autoCool = -1;
    }

    public void OnShoot()
    {
        canShootExt = playerControler.canShoot;

        if (canShootExt && canShoot && reloadCool <= 0)
        {

            if (shootCool <= 0 && loadedAmmo > 0)
            {
                muzzleFlash.Play();
                loadedAmmo--;
                Shoot();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Shoot,this.transform.position);
                shootCool = fireCool;
            }
            else if (loadedAmmo <= 0 && shootCool <= 0)
            {
                OnReload();
            }
        }
    }
    void SetAmmo()
    {
        float targetFill = (float)loadedAmmo / magSize;
        
        ammoCircle.fillAmount = Mathf.MoveTowards(ammoCircle.fillAmount, targetFill, Time.fixedDeltaTime * 1f);
        redCircle.fillAmount = ammoCircle.fillAmount + 0.07f;
        
        /*
        Generic code for handling target fill (DONT DELETE, KEEP THIS HERE)
        ammoCircle.fillAmount = (float)loadedAmmo / magSize;
        redCircle.fillAmount = (float)loadedAmmo / magSize + 0.07f;
         */
    }


    public void OnReload()
    {
        
        if (reloadCool <= 0)
        {
            canShoot = false;
            gunSpin = true;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Reload,this.transform.position);
            reloadCool = reloadCool;
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

    void Shoot()
    {
        
        GameObject shot = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
        shot.GetComponent<Rigidbody>().velocity = shootPoint.transform.forward * 60;
        
    }
}
