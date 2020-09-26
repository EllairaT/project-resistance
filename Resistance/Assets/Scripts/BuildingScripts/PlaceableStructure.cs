//using Mono.CecilX;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlaceableStructure : MonoBehaviour
{
    //reference the scriptable obj
    public Structure stats;
    public MeshRenderer mesh;
    private Material mat;
    private Material illegal;
    private Material preview;
    public bool isMoveable;
    public List<Collider> colliders = new List<Collider>();
    public bool isPreview = true;
    Renderer r;

    void Start()
    {
        r = mesh.GetComponent<Renderer>();
        illegal = Resources.Load("Materials/IllegalPreview") as Material;
        preview = Resources.Load("Materials/Preview") as Material;
       //.material = preview;
    }

    private void Update()
    {
        if (isPreview)
        {
            r.material = preview;         
        }
        else
        {
            isPreview = !isPreview;
        }
    }

    void PrintStats()
    {
        Debug.Log(mesh.name + " " + mat.name + "" +stats.cost);
    }


    public void AssignMaterial(Materials material)
    {
            mat = material.GetMaterial();
            r.material = mat;
            stats.CalculateCost(material.cost);
            stats.hardness = material.hardness;
        
    }

    public GameObject GetMesh()
    {
        return mesh.gameObject;
    }

     void OnTriggerEnter(Collider other)
    {
        if (isPreview)
        {
            if (other.CompareTag("Building"))
            {
                Debug.Log("colliding");
                colliders.Add(other);       
                r.material = illegal;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isPreview)
        {
            if (other.CompareTag("Building"))
            {
                colliders.Remove(other);
               // r.material = preview;
            }
        }
    }
}