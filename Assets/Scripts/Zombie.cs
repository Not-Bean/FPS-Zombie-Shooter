using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]

public class Zombie : MonoBehaviour
{
    public float speed;
    public float health;
    public float bulletDamage;
    public int followDist;
    public float strength;
    public int dropChance;
    public bool hasFullAnimations = false;
    public bool isExplosive = false; // Only explodes if this is true
    public float explosionRadius = 3f;
    public float explosionDamage = 25f;

    int deadCool = 1000;
    bool dead = false;
    int clock;

    bool attackPlayer;
    int attackCool;

    Vector3 wanderPoint;

    GameObject spawner;
    public Animator anim;

    GameObject player;
    private PlayerControler playerControler;
    Rigidbody rb;
    private StudioEventEmitter emitter;
    public GameObject bloodEffect;
    public GameObject explosionEffect;
    public GameObject ammoDrop;

    private void Start()
    {
        wanderPoint.x = transform.position.x + Random.Range(-10, 10);
        wanderPoint.z = transform.position.z + Random.Range(-10, 10);
        wanderPoint.y = transform.position.y - 1;

        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.Zombie, this.gameObject);
        emitter.Play();
        playerControler = player.GetComponent<PlayerControler>();

    }

    public void SpawnerSet(GameObject spawn)
    {
        spawner = spawn;
    }

    private void Update()
    {
        if (health <= 0 && !dead) // Death Code
        {
            
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ZombieDeath,this.transform.position);

            if (Random.Range(0,100) < dropChance)
            {
                Instantiate<GameObject>(ammoDrop, transform.position, Quaternion.identity);
            }

            emitter.Stop();

            if(hasFullAnimations && !isExplosive)
            {
                dead = true;
                anim.SetBool("isDead", true);
                spawner.GetComponent<Spawner>().dead(gameObject);

                GetComponent<CapsuleCollider>().enabled = false;

                deadCool = 300;
            }
            else if (isExplosive)
            {
                
                Explode();
            }
            else
            {
                spawner.GetComponent<Spawner>().dead(gameObject);
                Destroy(gameObject);
            }

        }

        if (deadCool < 900)
        {
            deadCool--;
            if (deadCool <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (!dead)
        {

            if (Vector3.Distance(player.transform.position, gameObject.transform.position) < followDist) // Check if player in range
            {
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
                rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, speed / 100));
            }
            else if (clock > 60) // Wander if not. (waits 1 second after spawning)
            {
                transform.LookAt(new Vector3(wanderPoint.x, transform.position.y, wanderPoint.z));
                rb.MovePosition(Vector3.MoveTowards(transform.position, wanderPoint, speed / 100));
            }

            if (isExplosive && Vector3.Distance(transform.position, player.transform.position) <= explosionRadius)
            {
                playerControler.Damage(explosionDamage);
                
                Explode();
            }
        }
    }
        


    private void FixedUpdate()
    {
        clock++;

        if (clock == 60) // randomize the clock after first second
        {
            clock += Random.Range(0, 600);
        }

        if (clock % 600 == 0) // Get new wander point 
        {
            wanderPoint.x = transform.position.x + Random.Range(-10, 10);
            wanderPoint.z = transform.position.z + Random.Range(-10, 10);
            wanderPoint.y = transform.position.y;
        }

        if (attackCool <= 0 && !attackPlayer && hasFullAnimations)
        {
            anim.SetBool("isAttacking", false);
        }

        if (attackPlayer && !dead)
        {
            if (attackCool <= 0)
            {
                if (hasFullAnimations)
                {
                    anim.SetBool("isAttacking", true);
                }

                player.GetComponent<PlayerControler>().Damage(strength);
                attackCool = 60;

                
            }
        }


        if (attackCool >= 0)
        {
            attackCool--;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && !dead)
        {
            health -= bulletDamage;
            ContactPoint contact = collision.contacts[0];
            Instantiate(bloodEffect, contact.point, Quaternion.LookRotation(contact.normal));
        }
        if (collision.gameObject.tag == "Player" && !dead)
        {
            attackPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !dead)
        {
            attackPlayer = false;
        }
    }

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        spawner.GetComponent<Spawner>().dead(gameObject);
        Destroy(gameObject);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.ZombieExplosion,this.transform.position);
    }
    


}
