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
        isBuildingPaused = false;
    }

    public void MakeRay()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = playerCam.ScreenPointToRay(mousePos);
        RaycastHit hit;
      
        if (previewgameObject != null)
        {
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 70f, layer))
            {
                /**some objects are unity primitive objects rather than imported from blender 
                so for unity objects, point (0,0,0) is at the center, instead of at the bottom 
                (which is considered the correct 0,0,0 position)
                this part is to take that into account.**/

                if (previewgameObject.CompareTag("primitive")) //so far, only foundations are unity primitive objects. I made a tag.
                {
                    //take the y value of the raycast and add it to half the height of the obj
                    float y = hit.point.y + (previewgameObject.transform.localScale.y / 2f);

                    previewgameObject.transform.position = SetGridPosition(hit, y);
                }
                else
                {
                    previewgameObject.transform.position = hit.point;
                }
            }
        }
    }

    private Vector3 SetGridPosition(RaycastHit hit, float _y)
    {
        float gridSize = 1;

        //-- true position
        Vector3 truePos = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(_y), Mathf.Round(hit.point.z))
        {
            x = Mathf.Floor(hit.point.x / gridSize) * gridSize,
            y = Mathf.Floor(hit.point.y / gridSize) * gridSize,
            z = Mathf.Floor(hit.point.z / gridSize) * gridSize
        };
        return truePos;
    }
}
