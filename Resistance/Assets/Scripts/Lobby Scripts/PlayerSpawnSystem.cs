using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawnSystem : NetworkBehaviour
{
    //Prefabs that can be spawned
    [SerializeField] private GameObject[] playerPrefab = null;
    [SerializeField] private GameObject targetPrefab = null;

    private static List<Transform> spawnPoints = new List<Transform>();

    private int nextIndex = 0;
    private int hostPlayer = 0;


    //Add spawn point to spawn system
    public static void AddSpawnPoint(Transform transform)
    {
        spawnPoints.Add(transform);

        spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }

    //Remove spawn point from spawn system
    public static void RemoveSpawnPoint(Transform transform) => spawnPoints.Remove(transform);

    public override void OnStartServer() => NetworkManagerLobby.OnServerReadied += SpawnPlayer;

    [ServerCallback]
    private void OnDestroy() => NetworkManagerLobby.OnServerReadied -= SpawnPlayer;

    [Server]
    public void SpawnPlayer(NetworkConnection conn, int character) //spawns player in the game
    {
        Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);
        if (spawnPoint == null)
        {
            Debug.LogError($"Missing spawn point for player {nextIndex}");
            return;
        }

        GameObject playerInstance;
        //if(hostPlayer == 0)
        //{
        //    playerInstance = Instantiate(playerPrefab[4], playerPrefab[4].gameObject.transform.position, playerPrefab[4].gameObject.transform.rotation);
        //    hostPlayer++;
        //}
        //else
        //{
            playerInstance = Instantiate(playerPrefab[character], spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
        //}

        NetworkServer.Spawn(playerInstance, conn);

       // GameObject targetInstance = Instantiate(targetPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
       // NetworkServer.Spawn(targetInstance, connectionToServer);

        nextIndex++; //next spawn point

    }
}