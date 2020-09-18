using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableStructure : MonoBehaviour
{
    //reference the scriptable obj
    public Structure stats;
    public MeshRenderer mesh;
    private Material mat;

    [HideInInspector] public List<Collider> colliders = new List<Collider>();
    void Start()
    {
    }
    void Update()
    {

    }

    void PrintStats()
    {
        Debug.Log(mesh.name + " " + stats.cost);
    }

    public void AssignMaterial(Materials material)
    {
        mesh.material = material.GetMaterial();
        stats.CalculateCost(material.cost);
        stats.hardness = material.hardness;
    }

    public GameObject GetMesh()
    {
        return mesh.gameObject;
    }

    //once structure has been placed on the map, the player will no longer be able to move the structure
    public void SetMoveable(bool b)
    {
        stats.moveable = b;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            colliders.Add(other);
            Debug.Log("added collider");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            Debug.Log("removed collider");
            colliders.Remove(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (stats.moveable)
        {

        }
    }
}