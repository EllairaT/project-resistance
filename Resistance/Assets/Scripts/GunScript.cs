using Mirror.Examples.Additive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//public class GunScript : MonoBehaviour
public class GunScript : NetworkBehaviour
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
        // Debug.Log("Local A: " + hasAuthority);
        // Debug.Log("Base A: " + base.hasAuthority);

        if(!base.hasAuthority)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {      
           Shoot();
        }
    }
 
    //new
    public void Shoot()
    {
        //Debug.Log(isServer + " IS SERVER?");
        //Debug.Log("pew");
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            Attackable target = hit.transform.GetComponent<Attackable>();
            //---Mirror
            Debug.Log("Going to Use");
           // target.Use(damage); //PUT IN NULL STATEMENT
            CmdUse(target.GetNetworkIdentity(), target.GetId());
            
            //---
            //if (target != null)
            //{
            //    if (!isServer)
            //    {
            //        Debug.Log("Am Client");
            //        target.CmdTakeDamage(damage);
            //    }
            //    else
            //    {
            //        Debug.Log("Am Server");
            //        target.RpcTakeDmg(damage);
            //    }
            //}
            
            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactGO, 2f);
        }
    }

    [Command]
    private void CmdUse(NetworkIdentity netIdent, int id)
    {
        INetworkUsable[] usables = netIdent.gameObject.GetComponents<INetworkUsable>();
        for (int i = 0; i < usables.Length; i++)
        {
            if (usables[i].GetId() == id)
            {
                Debug.Log("Found the ID! Calling Use");
                usables[i].Use(damage);
            }
        }
    }
}
