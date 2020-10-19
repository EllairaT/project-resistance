using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class DefenceSpawner : NetworkBehaviour
{
    [SerializeField] public GameObject[] defencePrefabs = null;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static explicit operator DefenceSpawner(GameObject v)
    {
        throw new NotImplementedException();
    }
}
