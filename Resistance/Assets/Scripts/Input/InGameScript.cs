using UnityEngine;
using Mirror;
using System.Collections;

public class InGameScript : NetworkBehaviour
{
    #region variables
    //Variables
    [Header("Gun")]
    public float damage = 10f;
    public float range = 400f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [Header("Death")]
    [SerializeField] public GameObject playerPrefab;
    public GameObject spectatorCamera;

    [Header("Ammo")]
    [SerializeField] public AmmoScript ammoScript;
    [SerializeField] public int maxClipAmmo = 20;
    [SerializeField] public int currentClipAmmo;
    [SerializeField] public int maxTotalAmmo = 100;
    [SerializeField] private int currentTotalAmmo;

    [Header("Gold")]
    [SerializeField] public GoldScript goldScript;
    [SerializeField] public int startingGold = 1000;
    [SerializeField] public int currentGold;

    [Header("Health")]
    [SerializeField] public HealthBarScript healthScript;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int currentHealth = 100;

    [Header("Building")]
    [SerializeField] public BuildManager buildManager;
    #endregion

    private void Awake()
    {
        currentHealth = maxHealth;
        healthScript.SetMaxHealth(currentHealth);

        currentGold = startingGold;
        goldScript.SetGold(currentGold);

        currentClipAmmo = maxClipAmmo;
        ammoScript.SetClipAmmo(currentClipAmmo);

        currentTotalAmmo = maxTotalAmmo;
        ammoScript.SetTotalAmmo(currentTotalAmmo);
    }

    #region Update method for user input
    void Update() //called every frame checking for user input
    {
        if (!base.hasAuthority)
        {
            return;
        }

        buildManager.ListenForInput();

        if (buildManager.isInventoryActive)
        {
            buildManager.playerInventory.ListenForInput();
            buildManager.playerInventory.ScrollThroughInventory();

            if (buildManager.buildSystem.isBuilding)
            {
            
                StartCoroutine(Build());
            }
        }
        else
        {
            Default();
        }
    }
    #endregion

    #region default user input implementation
    private void Default()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
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
                //Debug.Log("Out of Ammo!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.R)) //reload
        {
            Reload();
        }
        else if (Input.GetKeyDown(KeyCode.T)) //take damage
        {
            //TestHealthBar();
        }
    }
    #endregion

    #region build system implementation
    IEnumerator Build()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("In Game Script: " + base.hasAuthority + ", " + hasAuthority + ", " + base.isLocalPlayer + ", " + isLocalPlayer);
            buildManager.buildSystem.CmdBuild();
        }
        else if (Input.GetKeyDown(KeyCode.E)) //rotate
        {
            buildManager.buildSystem.previewgameObject.transform.Rotate(0, 90f, 0);
        }
        else
        {
            if (buildManager.buildSystem.isBuildingPaused) //whenever the preview is snapped, the buildsystem is paused
            {
                //to resume buildsystem, we need to "un-snap" 
                //unsnapping will occur when the mouse moves away a certain amount.
                float mX = Input.GetAxis("Mouse X");
                float mY = Input.GetAxis("Mouse Y");

                if (Mathf.Abs(mX) >= buildManager.buildSystem.stickTolerance || Mathf.Abs(mY) >= buildManager.buildSystem.stickTolerance)
                {
                    buildManager.buildSystem.isBuildingPaused = false;
                }
            }
            else
            {
                buildManager.buildSystem.MakeRay();
            }
        }
        yield return null;
    }
    #endregion

    #region shooting implementation
    public void Shoot()
    {
        Debug.Log("pew");
        muzzleFlash.Play();
        //TestHealthBar(); //method simply for testing healthing bar reducing

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Attackable target = hit.transform.gameObject.GetComponentInParent<Attackable>();

            if (target != null)
            {
                CmdUse(target.GetNetworkIdentity(), target.GetId());
                EarnGold(target);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactGO, 2f);
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
    #endregion

    #region test methods
    /*
    //Method to simply test health bar reduction
    private void TestHealthBar()
    {
        currentHealth -= 10;
        healthScript.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            spectatorCamera = GameObject.FindGameObjectWithTag("SpectatorCamera");
            if (spectatorCamera == null)
            {
                Debug.Log("NOT FOUND CAMERA");
            }
            else
            {
                Debug.Log("FOUND CAMERA");
            }
            spectatorCamera.gameObject.SetActive(true);
            spectatorCamera.SetActive(true);
            NetworkServer.Destroy(playerPrefab);
        }
    }

    public int TestEarnGold(int currentGold, int goldIncrease) //Unit Test method
    {
        return currentGold += goldIncrease;
    }
    */
    #endregion

    //Increase the player's gold based on the target hit's gold value
    private void EarnGold(Attackable targetHit)
    {
        int goldIncrease = targetHit.goldValuePerHit;
        currentGold += goldIncrease;
        goldScript.SetGold(currentGold);
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
