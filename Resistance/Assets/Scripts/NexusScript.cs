using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered  nexus buy range");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("pog");
    }
}
