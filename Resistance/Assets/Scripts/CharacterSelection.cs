using Mirror;
using UnityEngine;

public class CharacterSelection : NetworkBehaviour
{
    private NetworkRoomPlayerLobby roomLobby = null;

    public void SetRoomLobby(NetworkRoomPlayerLobby nrpl)
    {
        roomLobby = nrpl;
    }

    public void DisableCharacterSelect()
    {
        roomLobby.DisableCharacterSelect();
    }

    public void SetCharacterIndex(int character)
    {
        if (ClientScene.localPlayer.hasAuthority)
        {
            roomLobby.CmdSetCharacterIndex(character);
        }
    }
}
