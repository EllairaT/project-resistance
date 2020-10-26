using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BuildSystem : NetworkBehaviour
{
    public LayerMask layer;

    [Header("Set Up")]
    public Camera playerCam;
    public Material legalMaterial;
    public Material illegalMaterial;
    public Materials defaultMaterial;

    public Preview previewScript = null;
    public GameObject previewgameObject = null;
    private DefenceSpawner defenceSpawner;

    public float stickTolerance = 1.5f;
    public bool isBuilding = false;
    public bool isBuildingPaused = false;

    public bool IsBuilding()
    {
        return isBuilding;
    }

    public void BuildNow()
    {
        if (isBuilding)
        {
            if (previewScript.IsSnapped())
            {
                //Debug.Log("Build2 !");
                //previewScript.Place();
                //RpcBuild();
                CmdBuild(); //<-- use this one
            }
            else
            {
                CancelBuild();
            }

            if (isBuildingPaused)
            {

            }
        }
    }

    public void PauseBuild(bool _isPaused)
    {
        isBuildingPaused = _isPaused;
    }

    public void CancelBuild()
    {
        Destroy(previewgameObject);
        ResetAll();
    }

    //[Command]
    //public void CmdBuild()
    //{
    //    //Debug.Log(hasAuthority + ", " + isLocalPlayer + ", " + base.hasAuthority + ", " + base.isLocalPlayer + ", " + GetComponent<NetworkIdentity>().hasAuthority + ", " + GetComponent<NetworkIdentity>().isLocalPlayer);

    //    // Debug.Log("INSIDE BUILDSYSTEM, BUILD()");
    //    Debug.Log("Build System: " + base.hasAuthority + ", " + hasAuthority + ", " + base.isLocalPlayer + ", " + isLocalPlayer);
    //    //previewScript.CmdPlace();
    //    CmdPlace();
    //    ResetAll();
    //}

    public void NewBuild(GameObject _go)
    {
        if (previewgameObject != null)
        {
            CancelBuild();
        }

        previewgameObject = Instantiate(_go, Vector3.zero, Quaternion.identity);
        previewScript = previewgameObject.GetComponent<Preview>();
        previewScript.buildSystem = this;

        previewScript.legalMat = legalMaterial;
        previewScript.illegalMat = illegalMaterial;
        isBuilding = true;
        // Debug.Log("IS BUILDING IS SET TO TRUE");
    }

    //Working Method but when clients try spawn, is null preview script
    //If i remove command, works, but other players cant seem what other players (non-server) spawn
    //    [Command]
    //public void CmdPlace()
    //{
    //    Debug.Log("Attempting to Spawn");
    //    // Instantiate(prefab, transform.position, transform.rotation);
    //    GameObject temp = (GameObject)Instantiate(previewScript.prefab, previewScript.transform.position, previewScript.transform.rotation); //this was null
    //    NetworkServer.Spawn(temp);
    //    //previewScript.Destroy();
    //    NetworkServer.Destroy(previewScript.gameObject);
    //    //Destroy(gameObject);
    //}

    public void CmdBuild()
    {
        Structure temp = previewScript.prefab.GetComponent<Structure>();

        //Spawn defence
        CmdPlace(temp.index, previewScript.transform.position, previewScript.transform.rotation);

        previewScript.Destroy();
        ResetAll();
    }

    [Command]
    public void CmdPlace(int index, Vector3 pos, Quaternion rotation)
    {
        if (previewScript.IsSnapped())
        {
            if (defenceSpawner == null)
            {
                defenceSpawner = GameObject.FindGameObjectWithTag("DefenceSpawner").GetComponent<DefenceSpawner>();
            }
            GameObject temp = Instantiate(defenceSpawner.defencePrefabs[index], pos, rotation);
            NetworkServer.Spawn(temp);
        }
    }

    public void ResetAll()
    {
        previewgameObject = null;
        previewScript = null;
        isBuilding = false;
        // Debug.Log("IS BUILDIN RESET");
        isBuildingPaused = false;
    }

    //make code for custom anchor

    public void MakeRay()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = playerCam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (previewgameObject != null)
        {
            if (Physics.Raycast(ray, out hit, 100f, layer))
            {
                /**some objects are unity primitive objects rather than imported from blender 
                so for unity objects, point (0,0,0) is at the center, instead of at the bottom 
                (which is considered the correct 0,0,0 position)
                this part is to take that into account.**/

                if (previewgameObject.CompareTag("primitive")) //so far, only foundations are unity primitive objects. I made a tag.
                {
                    //take the y value of the raycast and add it to half the height of the obj
                    float y = hit.point.y + (previewgameObject.transform.localScale.y / 2f);
                    Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
                    pos.z += 5f;
                    previewgameObject.transform.position = pos;
                }
                else
                {
                    previewgameObject.transform.position = hit.point;
                }
            }
        }
    }
}

#region old implementation
//public Preview PreviewScript { get => previewScript; set => previewScript = value; }

//void Update()
//{
//if (hasAuthority)
//{
//    if (Input.GetKeyDown(KeyCode.R)) //rotate
//    {
//        previewgameObject.transform.Rotate(0, 90f, 0);
//    }

//    if (Input.GetMouseButtonDown(0) && isBuilding)//actually build the thing in the world
//    {
//        Debug.Log("Outside IF");
//        if (previewScript.IsSnapped())//is the previewGameObject currently snapped to anything? 
//        {
//            Debug.Log("Build !");
//            if(isServer)
//            {
//                Debug.Log("isServer TRUE");
//            }
//            else
//            {
//                Debug.Log("isServer FALSE");
//            }
//            //if so then stop the build and actually build it in the world
//            //RpcBuild();
//            CmdBuild();
//        }
//        else
//        {
//            Debug.Log("Not Snapped");//this part isn't needed, but may be good to give your player some feedback if they can't build
//        }
//    }

//    if (isBuilding)
//    {
//        if (isBuildingPaused) //whenever the preview is snapped, the buildsystem is paused
//        {
//            //to resume buildsystem, we need to "un-snap" 
//            //unsnapping will occur when the mouse moves away a certain amount.
//            float mX = Input.GetAxis("Mouse X");
//            float mY = Input.GetAxis("Mouse Y");

//            if (Mathf.Abs(mX) >= stickTolerance || Mathf.Abs(mY) >= stickTolerance)
//            {
//                isBuildingPaused = false;
//            }
//        }
//        else
//        {
//            MakeRay();
//        }
//    }

//}
//}
#endregion