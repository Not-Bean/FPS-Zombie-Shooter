using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{
    int clock;
    int offset;

    bool rotate = true;
    bool sinWave = true;


    void Start()
    {
        offset = Random.Range(0, 60);
        clock += offset;
    }

    private void Update()
    {
        if (sinWave)
        {
            transform.position = new Vector3(transform.position.x, transform.parent.position.y + Mathf.Sin((Time.time - offset) * 1) / 2, transform.position.z);
        }

        if (rotate)
        {
            transform.Rotate(0, 6 * 10 * Time.deltaTime, 0);
        }
        
        
    }

    private void FixedUpdate()
    {
        clock++;

        
    }
}
