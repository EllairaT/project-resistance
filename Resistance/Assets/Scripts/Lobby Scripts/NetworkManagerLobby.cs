using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerLobby : NetworkManager
{
    //Variables
    [SerializeField] private int minPlayers = 2;
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab = null;

    [Header("Game")]
    [SerializeField] private NetworkGamePlayerLobby gamePlayerPrefab = null;
    [SerializeField] private GameObject playerSpawnSystem = null;
    [SerializeField] private DefenceSpawner defenceSpawner = null;
    // [SerializeField] private GameObject roundSystem = null;

    private int playerIndexes = 0;
    private bool goingIntoGame = false;

    //Actions to handle clients
    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action<NetworkConnection, int> OnServerReadied;
    public static event Action OnServerStopped;

    public List<NetworkRoomPlayerLobby> RoomPlayers { get; } = new List<NetworkRoomPlayerLobby>(); //NetworkRoomPlayerLobby that each player will have
    public List<NetworkGamePlayerLobby> GamePlayers { get; } = new List<NetworkGamePlayerLobby>(); //NetworkGamePlayerLobby that each player will have

    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList(); //Load all spawnable prefabs

    //When client starts
    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab);
        }
    }

    //When client connects
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        OnClientConnected?.Invoke();
    }

    //When client disconnects
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        OnClientDisconnected?.Invoke();
    }

    //When server receives a connection, check if it can be valid
    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        if (SceneManager.GetActiveScene().path != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    //Add a player to the server
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            bool isLeader = RoomPlayers.Count == 0;
            Debug.Log("Instantiate RoomPlayerPrefab");
            NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);
            
            roomPlayerInstance.IsLeader = isLeader;

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }

    //When player disconnects from server
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();

            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }
        
        base.OnServerDisconnect(conn);
    }


    //When a server stops
    public override void OnStopServer()
    {
        OnServerStopped?.Invoke();
        RoomPlayers.Clear();
        GamePlayers.Clear();
    }

    //Updating 'Ready' value in lobby for each other player
    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    //Are all the player's ready?
    private bool IsReadyToStart()
    {
        if (numPlayers < minPlayers) { return false; }

        foreach (var player in RoomPlayers)
        {
            if (!player.IsReady) { return false; }
        }

        return true;
    }

    //Start the game
    public void StartGame()
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            if (!IsReadyToStart()) { return; }
            goingIntoGame = true;
            ServerChangeScene("MainScene");
        }
    }

    //Change scene from menu to the game
    public override void ServerChangeScene(string newSceneName)
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            for (int i = RoomPlayers.Count - 1; i >= 0; i--)
            {
                var conn = RoomPlayers[i].connectionToClient;
                var gameplayerInstance = Instantiate(gamePlayerPrefab);
                gameplayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);
                 
                int x = RoomPlayers[i].GetCharacterIndex(); //get selected player prefabs
                gameplayerInstance.SetCharacterIndex(x);

                NetworkServer.Destroy(conn.identity.gameObject);
                NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject, true);
            }
            var ds = Instantiate(defenceSpawner);
        }
        base.ServerChangeScene(newSceneName);
    }

    //When game is loaded (scene has been changed), spawn the players
    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName.StartsWith("MainScene"))
        {
            
            GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
            NetworkServer.Spawn(playerSpawnSystemInstance);

           // GameObject roundSystemInstance = Instantiate(roundSystem); //to be implemented
           // NetworkServer.Spawn(roundSystemInstance);
        }
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        int characterIndex = 0;
        if (goingIntoGame) //game has been told to start, getting player character index from each player (the prefab they chose)
        {
            characterIndex = GamePlayers[GamePlayers.Count - playerIndexes - 1].GetCharacterIndex();
            playerIndexes++;
        }
        base.OnServerReady(conn);
        OnServerReadied?.Invoke(conn, characterIndex); //Spawns player (calls SpawnSystem SpawnPlayer method)
    }
}