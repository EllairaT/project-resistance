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
    
}
