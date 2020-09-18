using UnityEngine;
using Mirror;

[RequireComponent(typeof(UsableIdAssigner))]
public class Attackable : NetworkBehaviour, INetworkUsable
{
    //Variables
    [SyncVar] private int id;
    [SyncVar] public float health = 50f;
    public bool isStructure;
    [SerializeField] public int goldValuePerHit = 10;
    //public GameObject attackableObject;

    void Start()
    {
        //attackableObject = GetComponent<PlaceableStructure>().GetMesh();
    }

    //Interface for all in game objects
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
    public void RpcTakeDmg(float amount) //Calls to reduce the object's health for all players in the game
    {
        health -= amount;
        Debug.Log("ouch");

        if (health <= 0f) //Is it dead? If so, it dies
        {
            Die();
        }
    }

    //In-game object's health has been set to <= 0, destroy the object in the server
    private void Die ()
    {
        NetworkServer.Destroy(gameObject);
    }
}