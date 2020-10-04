using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    //--referencing camera and layermask for the raycast
    public Camera playerCam;
    public LayerMask layer; //layer to ignore

    public GameObject previewgameObject = null;
    private Preview previewScript = null;

    public float stickTolerance = 1.5f;
    public bool isBuilding = false;
    private bool isBuildingPaused = false;

    public Material legalMaterial;
    public Material illegalMaterial;

    public Preview PreviewScript { get => previewScript; set => previewScript = value; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //rotate
        {
            previewgameObject.transform.Rotate(0, 90f, 0);
        }

        if (isBuilding)
        {
            if (isBuildingPaused) //whenever the preview is snapped, the buildsystem is paused
            {
                //to resume buildsystem, we need to "un-snap" 
                //unsnapping will occur when the mouse moves away a certain amount.
                float mX = Input.GetAxis("Mouse X");
                float mY = Input.GetAxis("Mouse Y");

                if (Mathf.Abs(mX) >= stickTolerance || Mathf.Abs(mY) >= stickTolerance)
                {
                    isBuildingPaused = false;
                }
            }
            else
            {
                MakeRay();
            }
        }
    }

    public void BuildNow()
    {
        if (isBuilding)
        {
            if (PreviewScript.GetIsSnapped())
            {
                Build();
            }
            else
            {
                CancelBuild();
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

    private void Build()
    {
        PreviewScript.Place();
        ResetAll();
    }

    public void NewBuild(GameObject _go)
    {
        previewgameObject = Instantiate(_go, Vector3.zero, Quaternion.identity);
        PreviewScript = previewgameObject.GetComponent<Preview>();
        PreviewScript.legalMat = legalMaterial;
        PreviewScript.illegalMat = illegalMaterial;
        isBuilding = true;
    }

    public void ResetAll()
    {
        previewgameObject = null;
        PreviewScript = null;
        isBuilding = false;
    }

    public void MakeRay()
    {
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //100f should be a set distance in front of the player
        //layer is included because the buildlayer will be ignored by raycast; if not set, glitching will happen       

        if (previewgameObject != null)
        {
            if (Physics.Raycast(ray, out hit, 100f, layer))
            {
                //some objects are unity primitive objects rather than imported from blender 
                //so point 0,0,0 is at the center, instead of at the bottom (which is considered the correct 0,0,0 position)
                //this part is to take that into account.
                if (previewgameObject.CompareTag("primitive"))
                {
                    float y = hit.point.y + (previewgameObject.transform.localScale.y / 2f); //take the y value of the raycast and add it to half the height of obj
                    Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
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
