using JetBrains.Annotations;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    public float health = 50f;
    public bool isStructure;
    public Structure structure;

    void Start()
    {
        if (isStructure)
        {
            health = structure.baseHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        if (isStructure)
        {
            amount = structure. CalculateDamage(amount);
        }

        health -= amount;
        Debug.Log("ouch");
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject);
    }
}
