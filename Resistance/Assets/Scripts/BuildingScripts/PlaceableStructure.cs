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
    public Material mat;
    private Material illegal;
    private Material preview;
    public List<Collider> colliders = new List<Collider>();
    public bool isMoveable;
    public bool isPreview;
    public bool isIllegal = false;
    public bool isPlaceable = false;
    public MeshRenderer r;

    void Start()
    {
        r = mesh.GetComponent<MeshRenderer>();
        illegal = Resources.Load("Materials/IllegalPreview") as Material;
        preview = Resources.Load("Materials/Preview") as Material;
    }

    private void Update()
    {
        r.material = isPreview ? (isIllegal ? illegal : preview) : mesh.material;

        mesh.GetComponent<MeshCollider>().enabled = !isPreview;  

        if ((!isPreview) && (GetComponent<Rigidbody>().velocity.y <= 0f))
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        }
    }

    void PrintStats()
    {
        Debug.Log(mesh.name + " " + mat.name + "" + stats.cost);
    }

    public void AssignMaterial(Materials material)
    {
        mat = material.GetMaterial();
        r.material = mat;
        stats.CalculateCost(material.cost);
        stats.hardness = material.hardness;
        isPreview = false;
        PrintStats();
    }

    public GameObject GetMesh()
    {
        return mesh.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Building"))
        {
            if(other is MeshCollider)
            {
                isIllegal = false;
            } 
            else if(other is BoxCollider)
            {
                isIllegal = true;
            }
        }
    }
 
    void OnTriggerExit(Collider other)
    {
       // Debug.Log("not colliding");
        if (isPreview)
        {
            if (other.CompareTag("Building"))
            {
                colliders.Remove(other);
                r.material = mat;
                isPlaceable = true;
                isIllegal = false;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Building"))
        {
            if (collision.collider is BoxCollider)
            {
                isIllegal = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Building"))
        {
            if (collision.collider is BoxCollider)
            {
                isIllegal = false;
            }
        }
    }
}