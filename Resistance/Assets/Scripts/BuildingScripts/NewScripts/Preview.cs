using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public GameObject prefab;

    private MeshRenderer prefabRend;

    [HideInInspector] public Material legalMat;
    [HideInInspector] public Material illegalMat;

    private BuildSystem bs; //reference to the building system

    private bool isSnapped = false;
    public bool isFoundation = false;

    public List<string> tagsSnappedTo = new List<string>();

    void Start()
    {
        //TODO: get parent's build system component
        bs = GameObject.FindObjectOfType<BuildSystem>(); 
        prefabRend = GetComponent<MeshRenderer>();
        ChangeMaterial();
    }

    void Update()
    {
        
    }

    public void Place()
    {
        Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void ChangeMaterial()
    {
        if (isSnapped)
        {
            prefabRend.material = legalMat;
        }
        else
        {
            prefabRend.material = illegalMat;
        }

        if (isFoundation)
        {
            prefabRend.material = legalMat;
            isSnapped = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < tagsSnappedTo.Count; i++)
        {
            string currentTag = tagsSnappedTo[i]; //get current tag 

            if(other.tag == currentTag) //if tag that was bumped into is equal to 
            {
                bs.PauseBuild(true); //snap! the build system must be paused when a snappoint is hit
                transform.position = other.transform.position;
                isSnapped = true;
                ChangeMaterial();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < tagsSnappedTo.Count; i++)
        {
            string currentTag = tagsSnappedTo[i];

            if(other.tag == currentTag)
            {
                isSnapped = false;
                ChangeMaterial();
            }
        }
    }

    public bool GetIsSnapped()
    {
        return isSnapped;
    }
}
