using JetBrains.Annotations;
using UnityEngine;
using Mirror;

//public class Attackable : MonoBehaviour
[RequireComponent(typeof(UsableIdAssigner))]
public class Attackable : NetworkBehaviour, INetworkUsable
{
    [SyncVar] private int id;
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

    #region INetworkUsable
    public void SetId(int value) { id = value; }
    public int GetId() { return id; }
    public NetworkIdentity GetNetworkIdentity() { return base.netIdentity; }
    public void Use(float healthDeduct)
    {
        Debug.Log("Attempting to Reduce Health");
        Debug.Log(base.isServer + " base is server? ");
        Debug.Log(isServer + " is server?");
        if(base.isServer)
        {
            RpcTakeDmg(healthDeduct);
        }
    }
    #endregion

    //[Command]
    //public void CmdTakeDamage(float amount)
    //{
    //    //Debug.Log(transform.root.gameObject.GetComponent<NetworkIdentity>().hasAuthority);

    //    if (isStructure)
    //    {
    //        amount = structure.CalculateDamage(amount);
    //    }

    //    RpcTakeDmg(amount);
    //}

    [ClientRpc]
    public void RpcTakeDmg(float amount)
    {
        health -= amount;
        Debug.Log("ouch");

        if (health <= 0f)
        {
            //RpcDie();
            Die();
        }
    }

   // [ClientRpc]
    private void Die ()
    {
        //Destroy(gameObject);
        NetworkServer.Destroy(gameObject);
    }
}