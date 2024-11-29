using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    int clock;
    int offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = Random.Range(0, 60);
        clock += offset;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin((Time.time - offset) * 1) / 2, transform.position.z);
    }

    private void FixedUpdate()
    {
        clock++;

        
    }
}
