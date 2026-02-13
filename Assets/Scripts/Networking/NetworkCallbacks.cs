using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCallbacks : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkObject playerPrefab;

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
            Debug.Log($"Player joined: {player}");
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        Vector2 move = PlayerInputActionsSingleton.Instance.Player.Move.ReadValue<Vector2>();

        PlayerInputData data = new PlayerInputData
        {
            move = move
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
