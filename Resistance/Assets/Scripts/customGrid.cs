﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class customGrid : MonoBehaviour
{
    public GameObject target;
    public GameObject structure;
    Vector3 truePos;
    public float gridSize;


    void LateUpdate() //runs after update function
    {
        truePos.x = Mathf.Floor(target.transform.position.x/gridSize)   * gridSize;
        truePos.y = Mathf.Floor(target.transform.position.y / gridSize) * gridSize;
        truePos.z = Mathf.Floor(target.transform.position.z / gridSize) * gridSize;

        structure.transform.position = truePos;
    }
}
