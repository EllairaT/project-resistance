using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkRoomPlayerLobby : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[4];
    [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[4];
    [SerializeField] private Button startGameButton = null;

    [Header("Character Select")]
    [SerializeField] public CharacterSelection characterSelectionPrefab = null;

    private CharacterSelection characterSelection;
    private GameObject mainMenuCanvas;
    private int selectedCharacter = 0;

    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string DisplayName = "Loading...";
    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;

    private bool isLeader;
    public bool IsLeader
    {
        set
        {
            isLeader = value;
            startGameButton.gameObject.SetActive(value);
        }
    }

    private NetworkManagerLobby room;
    private NetworkManagerLobby Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    //When the initial room player lobby is instantiated
    public override void OnStartAuthority()
    {
        CmdSetDisplayName(PlayerNameInput.DisplayName);
        lobbyUI.SetActive(true);
        mainMenuCanvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    //Allows player to select their character by bringing the Character Select
    public void EnableCharacterSelect()
    {
        characterSelection.gameObject.SetActive(true);
        lobbyUI.SetActive(false);
        mainMenuCanvas.SetActive(false);
    }
    
    //Goes back into lobby, removing the Character Select
    public void DisableCharacterSelect()
    {
        characterSelection.gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
        lobbyUI.SetActive(true);
    }

    [Command]
    public void CmdSetCharacterIndex(int character) //Sets the index of the selected character
    {
        selectedCharacter = character;
    }

    //Return's the selected index
    public int GetCharacterIndex()
    {
        return selectedCharacter;
    }

    //When a client joins the host
    public override void OnStartClient()
    {
        Room.RoomPlayers.Add(this);
        SetUpCharacterSelect();
        UpdateDisplay();
    }

    //Instantiates the character select
    public void SetUpCharacterSelect()
    {
        Debug.Log("Instantiate CS");
        characterSelection = Instantiate(characterSelectionPrefab);

        characterSelection.gameObject.SetActive(false);
        characterSelection.SetRoomLobby(this);
    }

    //When a client leave the lobby
    public override void OnStopClient()
    {
        Room.RoomPlayers.Remove(this);

        UpdateDisplay();
    }

    public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplay();
    public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();

    //Updating the UI for all player's when a player joins/leaves/ready's up/unready's
    private void UpdateDisplay()
    {
        if (!hasAuthority)
        {
            foreach (var player in Room.RoomPlayers)
            {
                if (player.hasAuthority)
                {
                    player.UpdateDisplay();
                    break;
                }
            }

            return;
        }

        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Waiting For Player...";
            playerReadyTexts[i].text = string.Empty;
        }

        for (int i = 0; i < Room.RoomPlayers.Count; i++)
        {
            playerNameTexts[i].text = Room.RoomPlayers[i].DisplayName;
            playerReadyTexts[i].text = Room.RoomPlayers[i].IsReady ?
                "<color=green>Ready</color>" :
                "<color=red>Not Ready</color>";
        }
    }

    //Start button that is only available to the host
    public void HandleReadyToStart(bool readyToStart)
    {
        if (!isLeader) { return; }

        startGameButton.interactable = readyToStart;
    }

    [Command]
    private void CmdSetDisplayName(string displayName) //Sets the display name of all players
    {
        DisplayName = displayName;
    }

    [Command]
    public void CmdReadyUp() //When a player ready's/unready's, this method is called
    {
        IsReady = !IsReady;
        Room.NotifyPlayersOfReadyState();
    }

    [Command]
    public void CmdStartGame() //Start game button pressed
    {
        if (Room.RoomPlayers[0].connectionToClient != connectionToClient) { return; }
        Room.StartGame();
    }
}