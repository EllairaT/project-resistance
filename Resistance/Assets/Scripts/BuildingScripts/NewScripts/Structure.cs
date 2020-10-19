
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;


public class Structure : MonoBehaviour
{
    public string type;
    public float hardness;
    public float baseHealth;
    public int cost;
    public List<GameObject> snapPoints;
    public int index;

    public float CalculateDamageTaken(float dmg)
    {
        return dmg / hardness;
    }

    public int CalculateCost(int matCost)
    {
        return matCost + cost;
    }   

    public void DisableSnapPoints()
    {
        
    }
}