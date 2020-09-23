using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "New Monster")]
public class Monster : ScriptableObject
{
    public float health;
    public float dmg;
    public float healing;

    public Type type;
    public int cost;

    //gold awarded to defenders when hitting the monster
    public int goldPerHit;

    //gold awarded to attacker when the monster attacks structure
    public int goldPerDmg;

    public int maxNumber;
    public int minNumber;
    public int numberToSpawn;

    public int TotalCost()
    {
        return cost * numberToSpawn;
    }

    public void TakeDamage(float d)
    {
        switch (type)
        {
            case Type.SUPPORT: //supps take 10% more damage
                d += 0.1f * d;
                break;
            case Type.TANK: //tanks take 15% less damage
                d -= 0.15f * d;
                break;
            case Type.DAMAGE: //fall through
            default:
                break;
        }

        health -= d;
    }
    
}

public enum Type
{
    DAMAGE,
    TANK,
    SUPPORT
}
