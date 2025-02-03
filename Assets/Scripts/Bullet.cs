using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int loadtime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "NoBullet")
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        loadtime++;
        if (loadtime > 600)
        {
            Destroy(gameObject );
        }
    }
}
