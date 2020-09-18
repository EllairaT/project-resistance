using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacement : MonoBehaviour
{
    private GameObject currentStructure;
    private GameObject Target;
    private int gridSize = 1;
    private Vector3 truePos;
    private Materials material;
    private int structureNumber = 0;
    private PlaceableStructure placeableBuilding;
    bool hasPlaced;
    Vector3 mousePos;
    Vector3 persp;

    void Update()
    {
        if (currentStructure != null)
        {
            placeableBuilding = currentStructure.GetComponent<PlaceableStructure>();
            Cursor.lockState = CursorLockMode.Confined;
            mousePos = Input.mousePosition;

            //-- mouseposition & perspective
            mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.y);
            persp = GetComponent<Camera>().ScreenToWorldPoint(mousePos);
            Target.transform.position = new Vector3(persp.x, persp.y, persp.z);

            //-- true position
            truePos = new Vector3(persp.x, persp.y, persp.z)
            {
                x = Mathf.Floor(Target.transform.position.x / gridSize) * gridSize,
                y = Mathf.Floor(Target.transform.position.y / gridSize) * gridSize,
                z = Mathf.Floor(Target.transform.position.z / gridSize) * gridSize
            };

            if (truePos.y < 0)
            {
                truePos.y = 0;
            }

            currentStructure.transform.position = truePos;


            if (Input.GetKeyDown("space"))
            {
                if (IsLegalPosition())
                {
                    hasPlaced = true;
                    this.PlaceItem(currentStructure, material);
                }
            }
        }
    }

    //--set preview
    public void SetItem(GameObject s, Materials m, Materials pm)
    {
        material = m;
        hasPlaced = false;
        Target = new GameObject("_target " + structureNumber++);
       
        currentStructure = s.GetComponent<PlaceableStructure>().GetMesh();

        
        currentStructure.GetComponent<PlaceableStructure>().AssignMaterial(pm);
        currentStructure = Instantiate(currentStructure);
        currentStructure.GetComponent<Rigidbody>().detectCollisions = false;
        currentStructure.transform.parent = Target.transform;
    }

    public void PlaceItem(GameObject s, Materials m)
    {
        GameObject instance = s.GetComponent<PlaceableStructure>().GetMesh();
        instance.GetComponent<PlaceableStructure>().AssignMaterial(m);
        instance = Instantiate<GameObject>(instance, currentStructure.transform.position, Quaternion.identity);
        instance.transform.parent = Target.transform;
        instance.GetComponent<Rigidbody>().useGravity = true;
        
        instance.GetComponent<PlaceableStructure>().SetMoveable(false);
        Destroy(currentStructure);
    }

    bool IsLegalPosition()
    {
        //colliding with something
        if(placeableBuilding.colliders.Count > 0)
        {
            Debug.Log("no");        
            currentStructure.GetComponent<PlaceableStructure>().GetMesh().GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            Destroy(placeableBuilding); 
            return false;
        }
        return true;
    }

    private void LateUpdate()
    {
        
    }
}
