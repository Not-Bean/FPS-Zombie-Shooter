using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    public Zombie z;
    
    public GameObject player;
    private NavMeshAgent zombie;

    void Start()
    {
        zombie = GetComponent<NavMeshAgent>();
        //get player from script so the zombies can track the player
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Vector3.Distance(player.transform.position, zombie.transform.position) < z.followDist) //check if zombie is within follow distance. if it's not, wander
        {
            zombie.destination = player.transform.position;
        }
    }
}
