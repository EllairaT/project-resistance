using UnityEngine;
using Mirror;

public class PlayerScript : NetworkBehaviour
{
    public string userName;
    public string userIP;
    public string characterName;
    public string role;
    public int gold;
    public Sprite icon;

    [SerializeField] public Canvas playerHUD;
    

    public void Awake()
    {
        Debug.Log("Base HA: " + base.hasAuthority);
        Debug.Log("Base LP: " + base.isLocalPlayer);
        Debug.Log(hasAuthority);
        Debug.Log(isLocalPlayer);

        NetworkIdentity test = GetComponent<NetworkIdentity>();
        if(test == null)
        {
            Debug.Log("NO NETIDEN");
        }
        else
        {
            Debug.Log("THERE IS ");
        }
        Debug.Log("PARENT: " + GetComponentInParent<NetworkIdentity>().hasAuthority);
        Debug.Log("PARENT: " + GetComponentInParent<NetworkIdentity>().isLocalPlayer);
    }
}
