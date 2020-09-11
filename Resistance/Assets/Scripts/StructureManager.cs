using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StructureManager : MonoBehaviour
{

    public Structure s;
    public Materials m;
    public void CreateStructure(GameObject spawnPoint)
    {
        s.AssignMaterial(m);
        s.InstantiateStructure(spawnPoint);
    }
}
