using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public GameObject prefab;
    public BuildSystem buildSystem;
    private MeshRenderer rend;

    [HideInInspector] public Material legalMat;
    [HideInInspector] public Material illegalMat;

    private bool isSnapped = false;//only this script should change this value

    public bool isFoundation = false;

    public List<string> tagsToSnapTo = new List<string>();
 
    private void Start()
    {
        buildSystem = FindObjectOfType<BuildSystem>();
        rend = GetComponent<MeshRenderer>();
        ChangeMat();
    }

    public void Place()
    {
        Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject);
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
            string currentTag = tagsToSnapTo[i];

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
            string currentTag = tagsToSnapTo[i];

            if (other.CompareTag(currentTag))
            {
                isSnapped = false;
                ChangeMat();
            }
        }
    }

    public bool IsSnapped()
    {
        return isSnapped;
    }
}
