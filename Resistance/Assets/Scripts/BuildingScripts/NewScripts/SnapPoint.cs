using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    private bool isSnapped;
    private bool snappedWith;

    public bool IsSnapped { get => isSnapped; set => isSnapped = value; }
    public bool SnappedWith { get => snappedWith; set => snappedWith = value; }

    public bool IsCurrentlySnapped()
    {
        return IsSnapped;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            //something?

        }
    }

}
