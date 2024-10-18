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

    bool attackPlayer;
    int attackCool;

    GameObject spawner;

    GameObject player;
    Rigidbody rb;
    private StudioEventEmitter emitter; 

    private void Start()
    {
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
        if (health <= 0)
        {
            spawner.GetComponent<Spawner>().dead(gameObject);
            Destroy(gameObject);
            emitter.Stop();
        }

        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < followDist)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, speed / 100));
        }
    }
        


    private void FixedUpdate()
    {
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
