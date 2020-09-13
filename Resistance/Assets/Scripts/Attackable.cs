﻿using UnityEngine;
using Mirror;

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
        if(base.isServer)
        {
            RpcTakeDmg(healthDeduct);
        }
    }
    #endregion

    [ClientRpc]
    public void RpcTakeDmg(float amount)
    {
        health -= amount;
        Debug.Log("ouch");

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die ()
    {
        NetworkServer.Destroy(gameObject);
    }
}