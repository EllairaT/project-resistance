using UnityEngine;
using Mirror;

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
        if(!base.hasAuthority)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {      
           Shoot();
        }
    }
 
    public void Shoot()
    {
        Debug.Log("pew");
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Target Hit: " + hit.transform.name);

            Attackable target = hit.transform.GetComponent<Attackable>();

            if (target != null)
            {
                CmdUse(target.GetNetworkIdentity(), target.GetId());
            }

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