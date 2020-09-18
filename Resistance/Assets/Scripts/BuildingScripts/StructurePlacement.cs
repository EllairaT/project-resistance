using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacement : MonoBehaviour
{
    private GameObject currentStructure;
    Vector3 mousePos;
    Vector3 persp;
    private GameObject Target;
    private int gridSize = 1;
    private Vector3 truePos;
    private Materials material;
    private Structure structure;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStructure != null)
        {
            Cursor.lockState = CursorLockMode.Confined;
            mousePos = Input.mousePosition;
            
            //-- mouseposition & perspective
            mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.y);
            persp = GetComponent<Camera>().ScreenToWorldPoint(mousePos);
            Target.transform.position = new Vector3(persp.x, persp.y, persp.z);

            //--true position
            truePos = new Vector3(persp.x, persp.y, persp.z)
            {
                x = Mathf.Floor(Target.transform.position.x / gridSize) * gridSize,
                y = Mathf.Floor(Target.transform.position.y / gridSize) * gridSize,
                z = Mathf.Floor(Target.transform.position.z / gridSize) * gridSize
            };

            if(truePos.y < 0)
            {
                truePos.y = 0;
            }

            currentStructure.transform.position = truePos;

            if (Input.GetKeyDown("space"))
            {
                this.PlaceItem(structure, material);
            }
        }
    }

    //--set preview
    public void SetItem(Structure s, Materials pm, Materials m)
    {
        Target = new GameObject("_target");
 
        structure = s;
        material = m;

        s.AssignMaterial(pm);

        //currentStructure = Instantiate(s.InstantiateStructure(persp));
        currentStructure.transform.parent = Target.transform;
    }

    public void PlaceItem(Structure s, Materials m)
    {
        s.AssignMaterial(m);
        //GameObject go = Instantiate<GameObject>(s.InstantiateStructure(persp), currentStructure.transform.position, Quaternion.identity);
        //go.transform.parent = Target.transform;

        Destroy(currentStructure);
        s = null;
        m = null;
    }
}
