using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material", menuName = "New Material")]
public class Materials : ScriptableObject
{
    public new string name;
    public int cost;
    public float hardness;
    public Material mat;
}