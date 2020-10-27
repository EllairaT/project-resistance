using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Preview : NetworkBehaviour
{
    public GameObject prefab;
    public BuildSystem buildSystem;
    private MeshRenderer rend;

    [HideInInspector] public Material legalMat;
    [HideInInspector] public Material illegalMat;

    private bool isSnapped = false;

    public bool isFoundation = false;

    public StructureType type;

    public List<StructureTags> tagsToSnapTo = new List<StructureTags>();
 
    private void Start()
    {
        buildSystem = FindObjectOfType<BuildSystem>();
        rend = GetComponent<MeshRenderer>();
        ChangeMat();
    }

    //[ClientRpc]
    //[Command]
    //public void CmdPlace() //THIS IS False, False, False, False AND THIS DOES NOT BELONG TO the player object //// THE PREVIEW SCRIPT THAT GETS USED DOES NOT BELONG TO THE PLAYER, RATHER, IT BELONGS TO THE BUILDING
    //{
    //    Debug.Log("Attempting to Spawn");
    //    Debug.Log("Preview: " + base.hasAuthority + ", " + hasAuthority + ", " + base.isLocalPlayer + ", " + isLocalPlayer);
    //    // Instantiate(prefab, transform.position, transform.rotation);
    //    GameObject temp = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
    //    NetworkServer.Spawn(temp, temp);

    //    //NetworkServer.Destroy(gameObject);
    //    Destroy(gameObject);

    //}

    public void Destroy()
    {
        Destroy(gameObject);
        //NetworkServer.Destroy(gameObject);
    }

    public void SetMaterial(Material m)
    {
        prefab.GetComponent<Renderer>().material = m;
    }

    private void ChangeMat()
    {
        if (isSnapped)
        {
            rend.material = legalMat;
        }
        else
        {
            rend.material = illegalMat;
        }

        if (isFoundation)
        {
            rend.material = legalMat;
            isSnapped = true; //forced true value for at least one of the foundations so that we can start building
        }
    }

    //enable snap
    private void OnTriggerEnter(Collider other)//this is what dertermins if you are snapped to a snap point
    {
        for (int i = 0; i < tagsToSnapTo.Count; i++)
        {
            string currentTag = tagsToSnapTo[i].ToString();

            if (other.CompareTag(currentTag))
            {
                //need to "pause" raycast otherwise the position will be overridden in the next frame
                //which makes it look like it never snapped
                buildSystem.PauseBuild(true);
                transform.position = other.transform.position;
                isSnapped = true;
                ChangeMat();
            }
        }
    }

    //disable snap
    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < tagsToSnapTo.Count; i++)
        {
            string currentTag = tagsToSnapTo[i].ToString();

            if (other.CompareTag(currentTag))
            {
                isSnapped = false;
                ChangeMat();
            }
        }
    }

    public void Place()
    {
        Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public bool IsSnapped()
    {
        return isSnapped;
    }
}
