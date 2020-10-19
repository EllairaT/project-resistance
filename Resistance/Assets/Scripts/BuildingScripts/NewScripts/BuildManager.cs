using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Mirror;
using UnityEngine.Rendering;

public class BuildManager : NetworkBehaviour
{

    public GameObject foundationPreview;//make sure that you include the preview of the gameobject you want to build
    public GameObject wallPreview;//make sure that you include the preview of the gameobject you want to build
    public GameObject inventoryUI;

    [SerializeField] public BuildSystem buildSystem;

    private bool isInventoryActive = false;
    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = inventoryUI.GetComponent<Inventory>();
        inventoryUI.SetActive(false);
    }

    public void ToggleInventory()
    {
        if (isInventoryActive)
        {
            inventoryUI.SetActive(true);
        }
        else
        {
            inventoryUI.SetActive(false);
        }
        isInventoryActive = !isInventoryActive;
    }
}



//private void Update()
//{
//    if(!base.hasAuthority)
//    {
//        return;
//    }

//    if (Input.GetKeyDown(KeyCode.B))
//    {
//        ToggleInventory();
//    }


//    //playerInventory.ListenForInput();
//    if (Input.GetKeyDown(KeyCode.F))
//    {
//        buildSystem.NewBuild(foundationPreview);
//    }
//    else if (Input.GetKeyDown(KeyCode.H))
//    {
//        buildSystem.NewBuild(wallPreview);
//    }

//}