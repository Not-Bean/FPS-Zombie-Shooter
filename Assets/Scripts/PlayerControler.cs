using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class PlayerControler : MonoBehaviour
{
    
    [SerializeField] GameObject Death;
    [SerializeField] GameObject HealthUI;
    [SerializeField] TextMeshProUGUI totalAmmoText;
    [SerializeField] TextMeshProUGUI loadedAmmoText;
    [SerializeField] Image ammoCircle;
    [SerializeField] Image redCircle;
    [SerializeField] Image HealthCircle;
    [SerializeField] Image HealthRedCircle;
    [SerializeField] Image BloodSplatter = null;
    [SerializeField] Image hurtRadial = null;
    

    public float health;
    public float healthTimer = 0.1f;
    public int regenRate = 1;
    public bool canRegen = false;
    private float healCooldown = 3.0f;
    private float maxHealCooldown = 3.0f;
    private bool startCooldown = false;
    public bool dead;
    bool canShoot = true;
    bool gunSpin;
    int rotationCount;
    public int ReloadCooldown;
    int reloadCool;
    public int ammoCount;
    public int loadedAmmo;
    public int magSize;
    public ParticleSystem muzzleFlash;

    float maxHealth;

    private void Awake()
    {
        Application.targetFrameRate = 1000;
    }

    private void Start()
    {
        maxHealth = health;
        SetHealth();
        HealthUI.SetActive(false);
    }

    public void Damage(float damage)
    {
        health -= damage;
        canRegen = false;
        StartCoroutine(HurtFlash());
        UpdateHealth();
        SetHealth();
        healCooldown = maxHealCooldown;
        startCooldown = true;
        HealthUI.SetActive(true);
    }

    void UpdateHealth()
    {
        Color splatterAlpha = BloodSplatter.color;
        splatterAlpha.a = 1 - (health / maxHealth);
        BloodSplatter.color = splatterAlpha;
    }

    IEnumerator HurtFlash()
    {
        hurtRadial.enabled = true;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.Hurt,this.transform.position);
        yield return new WaitForSeconds(healthTimer);
        hurtRadial.enabled = false;
    }

    private void Update()
    {
        if (startCooldown)
        {
            healCooldown -= Time.deltaTime;
            if (healCooldown <=0)
            {
                canRegen = true;
                startCooldown = false;
            }
        }

        if (canRegen)
        {
            if (health <= maxHealth - 0.01)
            {
                health += regenRate * Time.deltaTime;
                UpdateHealth();
            }
            else
            {
                health = maxHealth;
                healCooldown = maxHealCooldown;
                canRegen = false;
                HealthUI.SetActive(false);
            }
        }
        if ( health >= 0)
        {

        }
        

        if (health <= 0)
        {
            dead = true;
            AudioManager.instance.MuteAll(true);
        }

        if (dead)
        {
            
            Death.SetActive(true);
        }
    }

    private void FixedUpdate()
    {

        SetHealth();

    }  



  

    public void SetAmmo(int loadedAmmo, int magSize)
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
    
    void SetHealth()
    {
        float targetFill = health / maxHealth;

        HealthCircle.fillAmount = Mathf.MoveTowards(HealthCircle.fillAmount, targetFill, Time.fixedDeltaTime * 1f);
        HealthRedCircle.fillAmount = HealthCircle.fillAmount + 0.07f;

        /*
        Generic code for handling target fill (DONT DELETE, KEEP THIS HERE)
        HealthCircle.fillAmount = health / maxHealth;
        HealthRedCircle.fillAmount = health / maxHealth + 0.07f;
         */
    }

    public void ShootBlock(bool value)
    {
        canShoot = value;
    }

    

    public void OnJump()
    {
        if (dead)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
            
        }
    }
}
