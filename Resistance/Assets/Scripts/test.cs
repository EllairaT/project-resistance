using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation anim;
    void Start()
    {
       //anim = GetComponent<Animation>();
        foreach (AnimationState state in anim)
        {
            state.speed = 0.5F;
        }
    }

    // Update is called once per frame
    void Update()
    {
       _= anim.IsPlaying("attack") ? true : false;
    }
}
