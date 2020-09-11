using Mirror.Examples.Additive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [SerializeField] Transform hand;

    void Awake()
    {
        transform.SetParent(hand);
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {      
           Shoot();
        }
    }
    void Shoot()
    {
        Debug.Log("pew");
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Attackable target = hit.transform.GetComponent<Attackable>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
