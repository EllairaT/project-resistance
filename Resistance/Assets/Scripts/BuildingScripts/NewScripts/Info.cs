using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{
    public Materials mat;
    public TextMeshPro cost;
    public TextMeshPro hardness;

    void Start()
    {
        cost.SetText("cost: " + mat.cost);
        hardness.SetText("hardness: " + mat.hardness);
    }
}
