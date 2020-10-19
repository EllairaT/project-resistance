using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HUDAnim : MonoBehaviour
{
    public GameObject HUD;
    Animator anim;

    private bool isShowing = false;

    private void Awake()
    {
        anim = HUD.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleHUD();
        }
    }

    void ToggleHUD()
    {
        if (isShowing)
        {
            anim.SetBool("show", false);
        }
        else
        {
            anim.SetBool("show", true);
        }

        isShowing = !isShowing;
    }
}
