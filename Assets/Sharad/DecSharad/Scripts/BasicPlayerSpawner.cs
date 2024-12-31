using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;


public class BasicPlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{

    private NetworkRunner networkRunner;

    [SerializeField] private NetworkPrefabRef playerPrefab;

    private Dictionary<PlayerRef,NetworkObject> spawnedCharacters = new Dictionary<PlayerRef,NetworkObject>();

    [SerializeField] private Transform[] spawnPoints;
    private int nextSpawnIndex = 0;




    void Awake()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points are not configured. Please assign them in the Inspector.");
        }
    }


    public void Start()
    {
        if (PlayerPrefs.GetInt("GameMode") == 2)
        {
            PlayerJoinAsClient();
        }
        else
        {
            PlayerJoinAsHost();
        }
    }


    private async void StartGame(GameMode gameMode)
    {
        networkRunner = gameObject.GetComponent<NetworkRunner>();
        networkRunner.ProvideInput = true;
        await InitializeNetworkRunner(gameMode);
    }

    private async Task InitializeNetworkRunner(GameMode gameMode)
    {
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        await networkRunner.StartGame(new StartGameArgs()
        {
            GameMode = gameMode,
            SessionName = "MyRoom",
            Scene = scene,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }


    public void PlayerJoinAsClient()
    {
        if (networkRunner == null) 
        {
            StartGame(GameMode.Client); 
        }
    }

    public void PlayerJoinAsHost()
    {
        if (networkRunner == null)
        {
            StartGame(GameMode.Host);
        }
    }

  public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) 
    {

        if (networkRunner.IsServer) // Host Server
        {
            Transform spawnPoint = spawnPoints[nextSpawnIndex];
            nextSpawnIndex = (nextSpawnIndex + 1) % spawnPoints.Length;

            NetworkObject PlayerRef = networkRunner.Spawn(playerPrefab, spawnPoint.position, spawnPoint.rotation, player);
            //Vector3 playerPos = Vector3.zero;
            //NetworkObject PlayerRef = networkRunner.Spawn(playerPrefab, playerPos, Quaternion.identity, player);
            spawnedCharacters.Add(player, PlayerRef);
        }
    
    }
  
    
  public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {
        if (networkRunner != null)
        {
            if(spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            {
                networkRunner.Despawn(networkObject);
                spawnedCharacters.Remove(player);
            }
        }
    
    
    }
  
    
    
  public void OnInput(NetworkRunner runner, NetworkInput input) { }




  public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
  public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
  public void OnConnectedToServer(NetworkRunner runner) { }
  public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
  public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
  public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
  public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
  public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
  public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
  public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
  public void OnSceneLoadDone(NetworkRunner runner) { }
  public void OnSceneLoadStart(NetworkRunner runner) { }
  public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player){ }
  public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player){ }
  public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data){ }
  public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress){ }
}
