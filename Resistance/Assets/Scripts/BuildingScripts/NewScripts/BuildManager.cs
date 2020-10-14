﻿using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering;

public class BuildManager : MonoBehaviour
{

    public GameObject foundationPreview;//make sure that you include the preview of the gameobject you want to build
    public GameObject wallPreview;//make sure that you include the preview of the gameobject you want to build
    public GameObject inventoryUI;

    public BuildSystem buildSystem;

    private bool isInventoryActive = false;
    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = inventoryUI.GetComponent<Inventory>();
        inventoryUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }


    }

    void ToggleInventory()
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
