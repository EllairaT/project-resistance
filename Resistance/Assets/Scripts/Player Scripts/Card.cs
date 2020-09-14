using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AddOn/Card")]
public class Card : AddOns
{
    public GameObject monsterPrefab;
    public enum ClassType { Damage, Tank, Support };
    public ClassType type;

    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _heal;

    public float Health => _health;
    public float Damage => _damage; //damage dealt per second
    public float Heal => _heal;

    public int SetNumber;

    void Start()
    {

    }

    void Update()
    {
        
    }

    void Attack()
    {

    }

    public void CreateMob(GameObject[] spawnpoints)
    {
        //for (int i = 0; i < SetNumber; i++)
        //{
        //    Instantiate<GameObject>(card, spawnpoints.transform.position, Quaternion.identity);
        //}
    }
}
