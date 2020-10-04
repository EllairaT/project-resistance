using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    //--referencing camera and layermask for the raycast
    public Camera playerCam;
    public LayerMask layer;

    public GameObject previewgameObject = null;
    private Preview previewScript = null;

    public float stickTolerance = 1.5f;
    public bool isBuilding = false;
    private bool isBuildingPaused = false;

    public Material legalMaterial;
    public Material illegalMaterial;

    public Preview PreviewScript { get => previewScript; set => previewScript = value; }

    void Start()
    {
        
    }

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
                //need to unpause
                if (Input.GetKeyDown(KeyCode.C)) //press C to cancel current build 
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
        Debug.Log("new build");
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

        if(previewgameObject != null)
        {
            if(Physics.Raycast(ray, out hit, 100f, layer))
            {
                previewgameObject.transform.position = hit.point;
            }
        }
    }
}
