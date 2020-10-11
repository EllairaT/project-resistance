using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomGrid : MonoBehaviour
{
    public Vector3 truePos;
    [SerializeField] private float gridSize = 1;

    public GameObject Target { get; set; }
    public GameObject Structure { get; set; }


    void LateUpdate() //runs after update function
    {
        if(Target != null)
        {
            truePos.x = Mathf.Floor(Target.transform.position.x / gridSize) * gridSize;
            truePos.y = Mathf.Floor(Target.transform.position.y / gridSize) * gridSize;
            truePos.z = Mathf.Floor(Target.transform.position.z / gridSize) * gridSize;

            Structure.transform.position = truePos;
        }      
    }
}
