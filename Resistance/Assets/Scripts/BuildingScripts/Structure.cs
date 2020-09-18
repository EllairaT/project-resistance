using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Structure", menuName = "New Structure")]
public class Structure : ScriptableObject
{
    private static int structNum = 0;
    public new string name = "structure " + structNum++;
    public bool moveable = true;
    private Material mat;
    private float hardness;
    private GameObject sp;
    public float baseHealth;
    public int cost;

    void Awake()
    {
        
    }

    void OnEnable() 
    {
       // Debug.Log(this.name);
    }

    public float CalculateDamage(float dmg)
    {
        return dmg / hardness;
    }

    public void AssignMaterial(Materials material)
    {
        mat = material.mat;
        cost = material.cost;
        hardness = material.hardness; //set the hardness multiplier
    }

    public int CalculateCost(int matCost)
    {
        return matCost + cost;
    }


    //public GameObject InstantiateStructure(Vector3 p)
    //{


       
    //    meshRenderer.material = mat;

    //    return ;
    //}
}
