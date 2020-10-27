using UnityEngine;
using Mirror;

public class BuildManager : NetworkBehaviour
{

    [SerializeField] public BuildSystem buildSystem;

    public Inventory playerInventory;
    public GameObject inventoryUI;
    public bool isInventoryActive;

    private void Start()
    {
        //playerInventory = inventoryUI.GetComponent<Inventory>();
        inventoryUI.SetActive(false);
        isInventoryActive = false;
        playerInventory.SetUp();
    }

    public void ListenForInput()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleInventory();
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