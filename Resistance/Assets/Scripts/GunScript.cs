using UnityEngine;
using Mirror;

public class GunScript : NetworkBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    private float nextTimeToFire = 0f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [SerializeField] Transform hand;
    
    [SerializeField] AmmoScript ammoScript;
    [SerializeField] private int maxClipAmmo = 20;
    [SerializeField] private int currentClipAmmo;
    [SerializeField] private int maxTotalAmmo = 100;
    [SerializeField] private int currentTotalAmmo;

    void Awake()
    {
        transform.SetParent(hand);

        currentClipAmmo = maxClipAmmo;
        ammoScript.SetClipAmmo(currentClipAmmo);

        currentTotalAmmo = maxTotalAmmo;
        ammoScript.SetTotalAmmo(currentTotalAmmo);
    }

    void Update()
    {
        if(!base.hasAuthority)
        {
            return;
        }

        if (Input.GetButton("Fire1") && Time.time > nextTimeToFire)
        {      
            if (currentClipAmmo > 0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
                currentClipAmmo -= 1;
                ammoScript.SetClipAmmo(currentClipAmmo);
            }
            else
            {
                Debug.Log("Out of Ammo!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Reload()
    {
        if (currentTotalAmmo > 0)
        {
            int requiredAmmo = maxClipAmmo - currentClipAmmo;
            if (currentTotalAmmo >= requiredAmmo)
            {
                currentTotalAmmo -= requiredAmmo;
                currentClipAmmo += requiredAmmo;
            }
            else
            {
                currentClipAmmo += currentTotalAmmo;
                currentTotalAmmo = 0;
            }
            ammoScript.SetClipAmmo(currentClipAmmo);
            ammoScript.SetTotalAmmo(currentTotalAmmo);
        }
        else
        {
            Debug.Log("Purchase More Ammo!!");
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

            if(hit.rigidbody != null)
            {
                Debug.Log("Add Impact!");
                hit.rigidbody.AddForce(-hit.normal * impactForce);
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