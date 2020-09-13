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
    private NetworkIdentity ni = null;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [SerializeField] Transform hand;

    private void Start()
    {
        ni = transform.root.gameObject.GetComponent<NetworkIdentity>();
        if(ni == null)
        {
            Debug.Log("NUL");
        }
        else
        {
            Debug.Log("NOT NUL)");
        }

        if(ni.isServer)
        {
            Debug.Log("NI IS SERVER");
        }
        else
        {
            Debug.Log("Not server");
        }
    }

    void Awake()
    {
        transform.SetParent(hand);
    }

    void Update()
    {
        if(!transform.root.gameObject.GetComponent<NetworkIdentity>().hasAuthority)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {      
           CmdShoot();
        }
    }
 
    //new
    public void CmdShoot()
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
                if (!isServer)
                {
                    target.CmdTakeDamage(damage);
                }
                else
                {
                    Debug.Log("Am Server");
                    target.RpcTakeDmg(damage);
                }
            }
            
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
