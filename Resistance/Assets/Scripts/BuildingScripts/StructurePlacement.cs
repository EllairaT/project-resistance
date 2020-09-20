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
    private float startingHeight;
    private Materials material;
    private PlaceableStructure placeableBuilding;

    private Vector3 truePos;
    private Vector3 mousePos;
    private Vector3 persp;

    public bool isBuilding = false;
    

    private List<GameObject> placedObjects = new List<GameObject>();

    void Update()
    {
        if (currentStructure != null)
        {
            mousePos = Input.mousePosition;

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
                if (IsLegalPosition())
                {
                    Debug.Log("placed");
                  
                    PlaceItem();
                }
                else
                {
                    Debug.Log("nope");
                }

            }

            if (isBuilding == false)
            {
                Destroy(currentStructure.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {

    }

    //--set preview
    public void SetItem(GameObject s, Materials m)
    {
        material = m;
        Target = new GameObject("_target " + structureNumber++);

        placeableBuilding = s.GetComponent<PlaceableStructure>();
        //placeableBuilding.isMoveable = true;
        //placeableBuilding.isPreview = true;

        currentStructure = Instantiate(placeableBuilding.gameObject);
        currentStructure.GetComponent<Rigidbody>().detectCollisions = false;
        currentStructure.transform.parent = Target.transform;
       // currentStructure.GetComponent<MeshCollider>().enabled = false;
        //ToggleCollision();
    }

    public void PlaceItem()
    {
        placeableBuilding.isMoveable = false;
        //placeableBuilding.isPreview = false;

        instance = currentStructure.GetComponent<PlaceableStructure>().GetMesh();
        instance.GetComponent<PlaceableStructure>().AssignMaterial(material);
        instance = Instantiate<GameObject>(instance, currentStructure.transform.position, Quaternion.identity);

        instance.transform.parent = Target.transform;
        instance.GetComponent<Rigidbody>().useGravity = true;
       
        BoxCollider bx = instance.GetComponent<BoxCollider>();
        bx.size = new Vector3((bx.size.x + 0.0003f), (bx.size.y + 0.0003f), (bx.size.z + 0.0003f));

        GameObject temp = instance;
        placedObjects.Add(temp);
        Destroy(currentStructure.gameObject);

        ShowAll();
    }

    bool IsLegalPosition()
    {
        //colliding with something
        bool isLegal = false;
        if(placeableBuilding.colliders.Count > 0)
        {
            Debug.Log("no");        
            
            Destroy(Target);
            Destroy(currentStructure);

            isLegal = false;
        }
        else
        {
            isLegal = true;
        }

        foreach (Collider item in placeableBuilding.colliders)
        {
            Debug.Log("found");
        }
        return isLegal;
    }

    private void ToggleCollision()
    {
        instance.GetComponent<MeshCollider>().enabled = !instance.GetComponent<MeshCollider>().enabled;     
    }
    
    public void ShowAll()
    {
        int count = 0;
        foreach (var item in placedObjects)
        {
            Debug.Log(count++ + ": " + item.name);
        }
    }
}
