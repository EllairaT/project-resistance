using UnityEngine;
using Mirror;

public class GunScript : NetworkBehaviour
{
    //Variables
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    private float nextTimeToFire = 0f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [SerializeField] Transform hand;
    
    [Header("Ammo")]
    [SerializeField] public AmmoScript ammoScript;
    [SerializeField] public int maxClipAmmo = 20;
    [SerializeField] private int currentClipAmmo;
    [SerializeField] public int maxTotalAmmo = 100;
    [SerializeField] private int currentTotalAmmo;

    [Header("Gold")]
    [SerializeField] public GoldScript goldScript;
    [SerializeField] public int startingGold = 1000;
    [SerializeField] private int currentGold;

    [Header("Health")]
    [SerializeField] public HealthBarScript healthScript;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    
    //Set the starting values for each components
    void Awake()
    {
        transform.SetParent(hand);

        currentHealth = maxHealth;
        healthScript.SetMaxHealth(currentHealth);

        currentGold = startingGold;
        goldScript.SetGold(currentGold);

        currentClipAmmo = maxClipAmmo;
        ammoScript.SetClipAmmo(currentClipAmmo);

        currentTotalAmmo = maxTotalAmmo;
        ammoScript.SetTotalAmmo(currentTotalAmmo);
    }

    //Method for Unit Testing
    public int TestShoot(int currentAmmo)
    {
        return currentAmmo -= 1;
    }

    void Update() //called every frame checking for user input
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

    //Reload's the player's gun
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

    //Method call to get the player to shoot a bullet
    public void Shoot()
    {
        Debug.Log("pew");
        muzzleFlash.Play();
        //TestHealthBar(); //method simply for testing healthing bar reducing

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Target Hit: " + hit.transform.name);

            Attackable target = hit.transform.GetComponent<Attackable>();

            if (target != null)
            {
                CmdUse(target.GetNetworkIdentity(), target.GetId());
                EarnGold(target);       
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactGO, 2f);
        }
    }

    //Method to simply test health bar reduction
    private void TestHealthBar()
    {
        currentHealth -= 10;
        healthScript.SetHealth(currentHealth);
    }

    //Increase the player's gold based on the target hit's gold value
    private void EarnGold(Attackable targetHit)
    {
        int goldIncrease = targetHit.goldValuePerHit;
        currentGold += goldIncrease;
        goldScript.SetGold(currentGold);
    }

    public int TestEarnGold(int currentGold, int goldIncrease) //Unit Test method
    {
        return currentGold += goldIncrease;
    }

    //Method to call for an in-game object when they've been "shot"
    //This will call their use method, which is the "active" method when an object has been interacted with
    //It will reduce the object's health
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