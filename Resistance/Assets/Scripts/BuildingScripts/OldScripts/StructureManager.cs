using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StructureManager : MonoBehaviour
{
    public GameObject[] structures;
    private StructurePlacement structurePlacement;
    public Materials material;
    public Materials previewMaterial;  
    private bool isInBuildMode = false;

    void Start()
    {
        if (Display.displays.Length > 1)
        {
            Debug.Log(Display.displays);
            Display.displays[1].Activate();
        }
        
        structurePlacement = GetComponent<StructurePlacement>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            isInBuildMode = !isInBuildMode;
        }
        structurePlacement.isBuilding = isInBuildMode;
    }

    void OnGUI()
    {   
        if (isInBuildMode)
        {
            gameObject.GetComponentInParent<MouseLook>().enabled = false;
            transform.parent.parent.GetComponentInParent<PlayerNewCameraController>().enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            for (int i = 0; i < structures.Length; i++)
            {               
                if (GUI.Button(new Rect(Screen.width / 20, Screen.height / 15 + Screen.height / 12 * i, 100, 30), structures[i].name))
                {                                     
                    structurePlacement.SetItem(structures[i], material);             
                }
            }
        }
        else
        {
            gameObject.GetComponentInParent<MouseLook>().enabled = true;
            transform.parent.parent.GetComponentInParent<PlayerNewCameraController>().enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;   
        }
    }
}