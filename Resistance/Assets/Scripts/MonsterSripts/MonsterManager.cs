using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    #region Singleton
    public static MonsterManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject nexus;
}
