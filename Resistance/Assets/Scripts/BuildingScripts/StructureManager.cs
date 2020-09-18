using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StructureManager : MonoBehaviour
{
    public GameObject[] structures;
    private StructurePlacement structurePlacement;
    public Materials material;
    public Materials previewMaterial;
    private PlaceableStructure ps;

    void Start()
    {
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }

        structurePlacement = GetComponent<StructurePlacement>();
    }

    void OnGUI()
    {
        for (int i = 0; i < structures.Length; i++)
        {
            if (GUI.Button(new Rect(Screen.width / 20, Screen.height / 15 + Screen.height / 12 * i, 100, 30), structures[i].name))
            {
                structurePlacement.SetItem(structures[i], material, previewMaterial);

            }
        }
    }
}