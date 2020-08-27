using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    public PlayerManager pm;
    public GameObject entity;
    int instanceNumber = 1;

    void Start()
    {
        SpawnEntities();
    }

    void SpawnEntities()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < 4; i++)
        {
            GameObject currentEntity = Instantiate(entityToSpawn, i, Quaternion.Identity);
            instanceNumber++;
        }
    }


}
