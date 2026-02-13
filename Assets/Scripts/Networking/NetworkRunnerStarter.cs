using Fusion;
using UnityEngine;

public class NetworkRunnerStarter : MonoBehaviour
{
    public NetworkRunner runnerPrefab;

    private async void Start()
    {
        var runner = Instantiate(runnerPrefab);
        runner.ProvideInput = true;

        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = "TestSession",
            Scene = SceneRef.FromIndex(1),   // FORCE Gameplay
            SceneManager = runner.GetComponent<INetworkSceneManager>()
        });
    }
}