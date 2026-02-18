using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkCallbacks : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkObject playerPrefab;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
    private NetworkRunner _runner;


    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerJoined fired...");

        if (runner.IsServer)
            StartCoroutine(SpawnNextFrame(runner, player));
    }

    private System.Collections.IEnumerator SpawnNextFrame(NetworkRunner runner, PlayerRef player)
    {
        // Wait one frame so the simulation is fully initialized
        yield return null;
        Debug.Log("Spawning for PlayerRef: " + player + " | LocalPlayer: " + runner.LocalPlayer);
        Debug.Log("Server is spawning player...");
        runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
    }


    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        // Only collect input for the local player
        if (runner.LocalPlayer == null)
            return;

        Vector2 move = PlayerInputActionsSingleton.Instance.Player.Move.ReadValue<Vector2>();
        bool attackPressed = PlayerInputActionsSingleton.Instance.Player.Attack.WasPressedThisFrame();

        PlayerInputData data = new PlayerInputData
        {
            move = move,
            attackPressed = attackPressed
        };

        input.Set(data);
    }


    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason reason) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnUserSimulationMessage(NetworkRunner runner, PlayerRef player, ReliableKey key, float data) { }

    // YOUR VERSION OF FUSION USES THESE EXACT SIGNATURES:
    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("Fusion: Scene load started");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("Fusion: Scene load finished");
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }



}
