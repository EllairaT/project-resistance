﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Structure", menuName = "New Structure")]
public class Structure : ScriptableObject
{

    private static int structNum = 0;
    public new string name = "structure " + structNum++;
    private Material mat;

    [SerializeField]
    private MonoScript script;

    private float hardness;

    public GameObject structurePrefab;
    private GameObject clone;

    private GameObject sp;
    public float baseHealth;
    public int cost;
    
    void Awake()
    {

        //Debug.Log(this.name + " is awake");
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

    public void InstantiateStructure(GameObject spawnPoint)
    {
        sp = spawnPoint;
        GameObject gridObject = new GameObject("Grid Object: " + name);
        gridObject.AddComponent<Transform>();

        GameObject target = new GameObject("_target");
        target.transform.parent = gridObject.transform;

        GameObject clone = Instantiate<GameObject>(structurePrefab, sp.transform.position, Quaternion.identity);
        clone.transform.parent = target.transform;

        MeshRenderer meshRenderer = clone.GetComponent<MeshRenderer>();
        meshRenderer.material = mat;
    }
}
