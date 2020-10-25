using UnityEngine;
using Mirror;

public class BuildManager : NetworkBehaviour
{
    
    [SerializeField] public BuildSystem buildSystem;

    public GameObject inventoryUI;
    private bool isInventoryActive;
    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = inventoryUI.GetComponent<Inventory>();
        inventoryUI.SetActive(false);
        isInventoryActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleInventory();
        }

        if (isInventoryActive)
        {
            playerInventory.ListenForInput();
 
            playerInventory.ScrollThroughInventory();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("heya");
            }
        }
    }

    public void ToggleInventory()
    {
        if (isInventoryActive)
        {
            inventoryUI.SetActive(false);
            buildSystem.CancelBuild();
        }
        else
        {
            inventoryUI.SetActive(true);
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