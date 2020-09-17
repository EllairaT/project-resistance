using Mirror;
using UnityEngine;

public class NetworkGamePlayerLobby : NetworkBehaviour
{
    [SyncVar]
    private string displayName = "Loading...";

    private int selectedCharacter = 0; //player prefab selected

    private NetworkManagerLobby room;
    private NetworkManagerLobby Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    //Sets the selected character's index
    public void SetCharacterIndex(int character)
    {
        selectedCharacter = character;
    }

    //Return the character's index
    public int GetCharacterIndex()
    {
        return selectedCharacter;
    }

    //When instantiated, add to NetworkManagerLobby's GamePlayer's (list of NetworkGamePlayerLobby's)
    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        Room.GamePlayers.Add(this);
    }

    //When client disconnects, remove client from NetworkManagerLobby
    public override void OnStopClient()
    {
        Room.GamePlayers.Remove(this);
    }

    [Server]
    public void SetDisplayName(string displayName) //Sets the display name for this
    {
        this.displayName = displayName;
    }
}