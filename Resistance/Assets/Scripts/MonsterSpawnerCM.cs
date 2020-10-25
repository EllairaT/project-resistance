using UnityEngine;
using Mirror;

public class MonsterSpawnerCM : NetworkBehaviour
{
    [SerializeField] public GameObject[] monsterPrefabs = null;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
