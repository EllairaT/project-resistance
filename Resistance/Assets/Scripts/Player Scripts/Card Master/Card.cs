using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AddOn/Card")]
public class Card : AddOns
{
    public enum ClassType { DAMAGE, TANK, SUPPORT };
    public ClassType type;

    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _heal;

    //gold awarded to defenders when hitting the monster
    public int goldPerHit;

    //gold awarded to attacker when the monster attacks structure or player
    public int goldPerDmg;

    public int maxNumber;
    public int minNumber;
    public int numberToSpawn;
    public int numberSpawned;
    public GameObject prefab;

    public float Health { get => _health; set => _health = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public float Heal { get => _heal; set => _heal = value; }

    public float CalculateDamageTaken(float d)
    {
        switch (type)
        {
            case ClassType.SUPPORT: //supps take 10% more damage
                d += 0.1f * d;
                break;
            case ClassType.TANK: //tanks take 15% less damage
                d -= 0.15f * d;
                break;
        }
        return d;
    }

    public void TakeDamage(float d)
    {
        Health -= CalculateDamageTaken(d);
    }



    public int TotalCost()
    {
        return Cost * numberToSpawn;
    }

    public void AwardGold(PlayerScript cardmaster)
    {
        cardmaster.gold += goldPerDmg;
    }
}
