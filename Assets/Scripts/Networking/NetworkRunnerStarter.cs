using Fusion;
using UnityEngine;

public class NetworkRunnerStarter : MonoBehaviour
{
    private async void Start()
    {
        Debug.Log("NetworkRunnerStarter running in scene: " + gameObject.scene.name);

        var runner = GetComponent<NetworkRunner>();
        runner.ProvideInput = true;

        // Diagnostic: list all active runners BEFORE StartGame
        var runners = FindObjectsByType<NetworkRunner>(FindObjectsSortMode.None);
        Debug.Log("Active runners BEFORE StartGame: " + runners.Length);
        foreach (var r in runners)
            Debug.Log("Runner BEFORE: " + r.name + " | ID: " + r.GetInstanceID());

        Debug.Log("Starting game with mode: AutoHostOrClient");

        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = "TestSession",
            Scene = SceneRef.FromIndex(1),
            SceneManager = runner.GetComponent<INetworkSceneManager>()
        });

        Debug.Log($"Runner started. Mode: {runner.Mode}");

        //  ADD THESE THREE LOGS RIGHT HERE 
        Debug.Log("Runner scene: " + runner.gameObject.scene.name);
        Debug.Log("Runner.IsRunning: " + runner.IsRunning);
        Debug.Log("Runner.IsShutdown: " + runner.IsShutdown);

        // Diagnostic: list all active runners AFTER StartGame
        runners = FindObjectsByType<NetworkRunner>(FindObjectsSortMode.None);
        Debug.Log("Active runners AFTER StartGame: " + runners.Length);
        foreach (var r in runners)
            Debug.Log("Runner AFTER: " + r.name + " | ID: " + r.GetInstanceID());
    }
}