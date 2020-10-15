using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering;

public class BuildManager : BaseMonobehaviour
{

    public GameObject foundationPreview;//make sure that you include the preview of the gameobject you want to build
    public GameObject wallPreview;//make sure that you include the preview of the gameobject you want to build
    public GameObject inventoryUI;
    public BuildSystem buildSystem;
    public PlayerPurchase purchases;
    public Inventory playerInventory;

    private bool isInventoryActive;
    private GameObject preview;

    public GameObject Preview { get => preview; set => preview = value; }

    private void Awake()
    {
        isInventoryActive = false;
        inventoryUI.SetActive(false);
    }

    private void Start()
    {
        playerInventory.MatPurchases = purchases.MaterialPurchase;
        playerInventory.StrucPurchases = purchases.StructurePurchase;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }

        if (isInventoryActive)
        {
            playerInventory.ListenForInput();
        }
    }

    void ToggleInventory()
    {
        if (isInventoryActive)
        {
            inventoryUI.SetActive(false);
        }
        else
        {
            inventoryUI.SetActive(true);
        }
        isInventoryActive = !isInventoryActive;
    }
}
