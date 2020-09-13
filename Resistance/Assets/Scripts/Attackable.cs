using JetBrains.Annotations;
using UnityEngine;
using Mirror;
//public class Attackable : MonoBehaviour
public class Attackable : NetworkBehaviour
{
    [SyncVar] public float health = 50f;
    public bool isStructure;
    public Structure structure;

    void Start()
    {
        if (isStructure)
        {
            health = structure.baseHealth;
        }
    }

    [Command]
    public void CmdTakeDamage(float amount)
    {
        //Debug.Log(transform.root.gameObject.GetComponent<NetworkIdentity>().hasAuthority);

        if (isStructure)
        {
            amount = structure.CalculateDamage(amount);
        }

        RpcTakeDmg(amount);
//        health -= amount;
//        Debug.Log("ouch");

        if (health <= 0f)
        {
            RpcDie();
        }
    }

    [ClientRpc]
    public void RpcTakeDmg(float amount)
    {
        health -= amount;
        Debug.LogError("ouch");
    }

    [ClientRpc]
    private void RpcDie ()
    {
        //Destroy(gameObject);
        NetworkServer.Destroy(gameObject);
    }
}