using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject playerInventory;
    public BuildSystem bs;
    private Inventory i;

    private bool hasPreviewSpawned = false;
    GameObject oldPreview = null;

    void Start()
    {
        playerInventory.SetActive(false);
        i = playerInventory.GetComponent<Inventory>();
    }

    void Update()
    {
        i.ListenForInput();

        //Scroll through inventory and update preview-------------

        if (i.CurrentlyActive != null && i.isInBuildMode)
        {
            i.ScrollThroughInventory(i.CurrentList);

            if (i.previewItem != null)
            {
                UpdatePreview(i.previewItem);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) //TODO check if position was valid
            {
                bs.BuildNow();
                i.ResetAll();
                hasPreviewSpawned = false;
            }
        }

        if (!i.isInBuildMode)
        {
            i.ResetAll();
        }

        //Stop building------------------------------------------
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
            bs.CancelBuild();
        }
    }

    void ToggleInventory()
    {
        playerInventory.SetActive(!playerInventory.activeSelf);
    }

    void UpdatePreview(GameObject _o)
    {
        if (oldPreview != null) //does a previous preview exist?
        {
            if (_o != oldPreview) //check if the user pressed 1,2,3,4,5, f
            {
                if (hasPreviewSpawned) 
                {
                    bs.CancelBuild(); //remove other preview
                    hasPreviewSpawned = false;
                }
                ShowItemPreview(_o); //then show the new one
            }
        }
        else
        {
            if (hasPreviewSpawned)
            {
                Debug.Log("here?");
                bs.CancelBuild();
                hasPreviewSpawned = false;
            }
                ShowItemPreview(_o);
        }
    }

    void ShowItemPreview(GameObject _o)
    {
        bs.NewBuild(_o);
        oldPreview = _o;
        hasPreviewSpawned = true; //prevent preview from infinitely spawning
    }
}
