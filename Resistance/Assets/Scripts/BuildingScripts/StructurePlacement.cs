using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StructurePlacement : MonoBehaviour
{
    private GameObject currentStructure;
    private GameObject Target;
    private GameObject instance;

    private int gridSize = 1;
    private int structureNumber = 0;
    private Materials material;
    private PlaceableStructure placeableBuilding;

    private Vector3 truePos;
    private Vector3 mousePos;
    private Vector3 persp;

    public bool isBuilding = false;
    

    private List<GameObject> placedObjects = new List<GameObject>();

    void Update()
    {
        int placedObjCount = 0;
        if (currentStructure != null)
        {
            mousePos = Input.mousePosition;
            placeableBuilding = currentStructure.GetComponent<PlaceableStructure>();
            placeableBuilding.isPreview = true; 
            //-- mouseposition & perspective
            mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.y);
            persp = GetComponent<Camera>().ScreenToWorldPoint(mousePos);
            Target.transform.position = new Vector3(persp.x, persp.y, persp.z);

            //-- true position
            truePos = new Vector3(Mathf.Round(persp.x), Mathf.Round(persp.y), Mathf.Round(persp.z))
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
                for (int i = 0; i < currentStructure.GetComponent<PlaceableStructure>().colliders.Count; i++)
                {
                    Debug.Log(i.ToString());
                }

                if (IsLegalPosition())
                {
                    Debug.Log("placed");                  
                    PlaceItem();
                   
                    placeableBuilding.isPreview = false;
                    placeableBuilding = null;
                }
                else
                {
                    Debug.Log("nope");
                }
                placeableBuilding = null;
            }

            if (isBuilding == false)
            {
                Destroy(placeableBuilding);
                Destroy(Target.gameObject);
                Destroy(currentStructure.gameObject);
            }
        }
    }


    //--set preview
    public void SetItem(GameObject s, Materials m)
    {
        material = m;
        Target = new GameObject("_target " + structureNumber++);

        currentStructure = Instantiate(s.gameObject);
        currentStructure.GetComponent<Rigidbody>().isKinematic = false;
        currentStructure.GetComponent<PlaceableStructure>().isPreview = true;
        currentStructure.transform.parent = Target.transform;
    }

    public void PlaceItem()
    {
        instance = placeableBuilding.GetMesh();
        instance.GetComponent<PlaceableStructure>().AssignMaterial(material);
        
        instance.GetComponent<Rigidbody>().useGravity = true;

        instance = Instantiate<GameObject>(instance, currentStructure.transform.position, Quaternion.identity);

        instance.transform.parent = Target.transform;
     
        BoxCollider bx = instance.GetComponent<BoxCollider>();
        bx.size = new Vector3((bx.size.x - 0.0003f), (bx.size.y - 0.0003f), (bx.size.z - 0.0003f));
        placedObjects.Add(instance);
        
        Destroy(currentStructure.gameObject);   

 
    }

    bool IsLegalPosition()
    {
        bool isLegal;
        if (placeableBuilding.isIllegal)
        {                     
            Destroy(Target);
            Destroy(currentStructure);
            isLegal = false;
        }
        else
        {
            isLegal = true;
        }
        return isLegal;
    }

}
