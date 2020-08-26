using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender1 : MonoBehaviour
{
    Defender df;
    // Start is called before the first frame update
    void Start()
    {    
        df = new Defender();
    }

    // Update is called once per frame
    void Update()
    {
        df.Move();
    }
}
