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

        //-----------INVENTORY------------------------------------------

        if (i.CurrentlyActive != null && i.isInBuildMode)
        {
            i.ScrollThroughInventory(i.CurrentList);

            if (i.previewItem != null)
            {
                if (hasPreviewSpawned == false)
                {
                    ShowItemPreview(i.previewItem);
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                if(oldPreview != null)
                {
                    bs.CancelBuild();
                }
                hasPreviewSpawned = false;
            }

            if (Input.GetKeyDown(KeyCode.Space)) //TODO check if position was valid
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
        //---BUILD SYSTEM----------------------------------------------------------

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

    void ShowItemPreview(GameObject _o)
    {
        bs.NewBuild(_o);
        oldPreview = _o;
        hasPreviewSpawned = true; //prevent preview from infinitely spawning
    }
}
