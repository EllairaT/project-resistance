using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public GameObject foundationPreview;//make sure that you include the preview of the gameobject you want to build
    public GameObject wallPreview;//make sure that you include the preview of the gameobject you want to build
    
    public BuildSystem buildSystem;

    public Inventory playerInventory;

    private void Update()
    {
        playerInventory.ListenForInput();
    }
}
