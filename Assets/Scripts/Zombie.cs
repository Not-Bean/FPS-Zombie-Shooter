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
    int dropChance;

    int clock;

    bool attackPlayer;
    int attackCool;

    Vector3 wanderPoint;

    GameObject spawner;

    GameObject player;
    Rigidbody rb;
    private StudioEventEmitter emitter; 

    private void Start()
    {
        wanderPoint.x = transform.position.x + Random.Range(-10, 10);
        wanderPoint.z = transform.position.z + Random.Range(-10, 10);
        wanderPoint.y = transform.position.y - 1;

        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.Zombie, this.gameObject);
        emitter.Play();
    }

    public void SpawnerSet(GameObject spawn)
    {
        spawner = spawn;
    }

    private void Update()
    {
        if (health <= 0) // Death Code
        {
            spawner.GetComponent<Spawner>().dead(gameObject);

            if (Random.Range(0,100) < dropChance)
            {
                // put item drop code here
            }

            Destroy(gameObject);
            emitter.Stop();
        }

        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < followDist) // Check if player in range
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, speed / 100));
        }
        else if (clock > 60) // Wander if not. (waits 1 second after spawning)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, wanderPoint, speed / 100));
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

        if (attackPlayer)
        {
            if (attackCool <= 0)
            {
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
        if (collision.gameObject.tag == "Bullet")
        {
            health -= bulletDamage;
        }
        if (collision.gameObject.tag == "Player")
        {
            attackPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackPlayer = false;
        }
    }


    



}
