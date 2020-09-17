using Mirror;

public class CharacterSelection : NetworkBehaviour
{
    private NetworkRoomPlayerLobby roomLobby = null;

    //Set the NetworkRoomPlayerLobby the character select belongs to
    public void SetRoomLobby(NetworkRoomPlayerLobby nrpl)
    {
        roomLobby = nrpl;
    }

    //Hides the character select
    public void DisableCharacterSelect()
    {
        roomLobby.DisableCharacterSelect();
    }

    //Sets the character chosen (their index value in the array)
    public void SetCharacterIndex(int character)
    {
        if (ClientScene.localPlayer.hasAuthority)
        {
            roomLobby.CmdSetCharacterIndex(character);
        }
    }
}
