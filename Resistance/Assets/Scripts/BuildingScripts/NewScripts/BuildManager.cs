using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public GameObject foundationPreview;//make sure that you include the preview of the gameobject you want to build
    public GameObject wallPreview;//make sure that you include the preview of the gameobject you want to build
    
    public BuildSystem buildSystem;

    private void Update()
    {
    //    if (Input.GetKeyDown(KeyCode.B)) //user disables build mode
    //    {
    //        isInBuildMode = false;
    //    }
    //    else
    //    {
    //        if (Input.GetKeyDown(KeyCode.Alpha1))
    //        {
    //            CurrentList = purchases.BlockArr;
    //            CurrentlyActive = blocks;
    //        }
    //        if (Input.GetKeyDown(KeyCode.Alpha2))
    //        {
    //            CurrentList = purchases.FenceArr;
    //            CurrentlyActive = fences;
    //        }
    //        if (Input.GetKeyDown(KeyCode.Alpha3))
    //        {
    //            CurrentList = purchases.GatesArr;
    //            CurrentlyActive = gates;
    //        }
    //        if (Input.GetKeyDown(KeyCode.Alpha4))
    //        {
    //            CurrentList = purchases.StairsArr;
    //            CurrentlyActive = stairs;
    //        }
    //        if (Input.GetKeyDown(KeyCode.Alpha5))
    //        {
    //            CurrentList = purchases.WallsArr;
    //            CurrentlyActive = walls;
    //        }
    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            CurrentList = purchases.FoundationArr;
    //            CurrentlyActive = foundation;
    //        }
    //        isInBuildMode = true;
    //    }
    }
}
