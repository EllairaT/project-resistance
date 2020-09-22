using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Structure", menuName = "New Structure")]
public class Structure : ScriptableObject
{
    public float hardness;
    public float baseHealth;
    public int cost;

    public float CalculateDamageTaken(float dmg)
    {
        return dmg / hardness;
    }

    public int CalculateCost(int matCost)
    {
        return matCost + cost;
    }   
}