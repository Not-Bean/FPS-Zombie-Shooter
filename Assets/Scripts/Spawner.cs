using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    [SerializeField] GameObject scoreKeeper;

    GameObject player;

    public int startingAmount;
    public int spawnDelay;
    public int maxZombies;
    public float spawnRange;

    int spawnCool;

    bool spawning = true;

    List<GameObject> zombies = new List<GameObject> { };

    // Start is called before the first frame update
    void Start()
    {
        if (spawning)
        {
            for (int i = 0; i < startingAmount; i++)
            {
                GameObject zomb = Instantiate(zombie, new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)) + transform.position, Quaternion.identity);
                zomb.GetComponent<Zombie>().SpawnerSet(gameObject);
                zombies.Add(zomb);
            }
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (zombies.Count < maxZombies && spawnCool <= 0 && spawning)
        {
            GameObject zomb = Instantiate(zombie, new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)) + transform.position, Quaternion.identity);
            zomb.GetComponent<Zombie>().SpawnerSet(gameObject);
                
            zombies.Add(zomb);
            spawnCool = spawnDelay;

            if (Vector2.Distance(player.transform.position, zomb.transform.position) < 15)
            {
                Destroy(zomb);
            }
        }
        else
        {
            spawnCool--;
        }
        
    }

    public void dead(GameObject dead)
    {
        zombies.Remove(dead);
        scoreKeeper.GetComponent<Kills>().AddKill();
    }
}
