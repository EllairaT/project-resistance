﻿using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawnSystem : NetworkBehaviour
{
    [SerializeField] private GameObject[] playerPrefab = null;

    private static List<Transform> spawnPoints = new List<Transform>();

    private int nextIndex = 0;

    public static void AddSpawnPoint(Transform transform)
    {
        spawnPoints.Add(transform);

        spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }
    public static void RemoveSpawnPoint(Transform transform) => spawnPoints.Remove(transform);

    public override void OnStartServer() => NetworkManagerLobby.OnServerReadied += SpawnPlayer;

    [ServerCallback]
    private void OnDestroy() => NetworkManagerLobby.OnServerReadied -= SpawnPlayer;

    [Server]
    public void SpawnPlayer(NetworkConnection conn, int ch)
    {
        Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);
        if (spawnPoint == null)
        {
            Debug.LogError($"Missing spawn point for player {nextIndex}");
            return;
        }
        GameObject playerInstance = Instantiate(playerPrefab[ch], spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
        NetworkServer.Spawn(playerInstance, conn);

        nextIndex++;
    }
}