using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldWeapon : MonoBehaviour
{
    public Transform leftBone;
    public Transform leftReference;
    public Transform rightBone;
    public Transform rightReference;
    public Transform elbowBone;
    public Transform elbowReference;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        leftBone.transform.parent = leftReference.transform;
        rightBone.transform.parent = rightReference.transform;
        elbowBone.transform.parent = elbowReference.transform;
        
    }
}
