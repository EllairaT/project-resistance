using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StructureManager : MonoBehaviour
{

    public Structure s;
    public void CreateStructure(GameObject spawnPoint)
    {
        s.GetStructure(spawnPoint);
    }
}
