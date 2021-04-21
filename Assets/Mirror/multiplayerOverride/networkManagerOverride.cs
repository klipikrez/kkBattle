using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mirror.multiplayerOverride
{
    public class networkManagerOverride : NetworkManager
    {
        //[Scene] [SerializeField] private string menuScene;

        public GameObject lobbyPlayer;

        public GameObject gamePlayer;

        public List<playerMultiplayer> playersInLobby { get; } = new List<playerMultiplayer>();

        public List<playerMultiplayerGame> GamePlayers { get; } = new List<playerMultiplayerGame>();
        public List<GameObject> GameObjects { get; } = new List<GameObject>();
        public List<Color> colors  = new List<Color>();


        public GameObject multiplayerPannel;

        [SerializeField]private GameObject playerSpawnSystem;

        public static event Action<NetworkConnection> OnServerReadied;
        /*
        public override void OnStartServer()
        {
            Debug.Log("promena dodaj");
            NetworkServer.SpawnObjects();
            
        }*/

        public bool singleplayer = false;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {


            if (SceneManager.GetActiveScene().name == "main menu")
            {
                bool isLeader = playersInLobby.Count == 0;

                GameObject roomPlayerInstance = Instantiate(lobbyPlayer);

                roomPlayerInstance.GetComponent<playerMultiplayer>().IsLeader = isLeader;

                NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);

                  //  roomPlayerInstance.GetComponent<playerMultiplayer>().multiplayerPannel = multiplayerPannel;
                
            }


        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            if (conn.identity != null)
            {
                var player = conn.identity.GetComponent<playerMultiplayer>();

                playersInLobby.Remove(player);

            }
            
            base.OnServerDisconnect(conn);
        }

        public override void OnStopServer()
        {

            playersInLobby.Clear();
            base.OnStopServer();
        }

        public void StartGame(string scene)
        {
            
            if (SceneManager.GetActiveScene().name == "main menu")
            {
               
                ServerChangeScene(scene);
            }
        }

        public override void ServerChangeScene(string newSceneName)
        {

            // From menu to game
            if (SceneManager.GetActiveScene().name == "main menu" && newSceneName.StartsWith("multiplayer"))
            {
                for (int i = playersInLobby.Count - 1; i >= 0; i--)
                {
                    if (playersInLobby.Count != 1)
                    {
                        colors.Add(playersInLobby[i].color);
                    }
                    else
                    {
                        colors.Add(Color.red);
                    }
                    var conn = playersInLobby[i].connectionToClient;
                    var gameplayerInstance = Instantiate(gamePlayer);
                    gameplayerInstance.GetComponent<playerMultiplayerGame>().SetDisplayName(playersInLobby[i].username);
                    gameplayerInstance.GetComponent<playerMultiplayerGame>().SetDisplayColor(playersInLobby[i].color);
                    gameplayerInstance.GetComponent<playerMultiplayerGame>().SetID(i);

                    NetworkServer.Destroy(conn.identity.gameObject);

                    NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject);
                }
            }

                base.ServerChangeScene(newSceneName);
            
        }
        
        public override void OnServerSceneChanged(string sceneName)
        {
            if (/*sceneName.StartsWith("Scene_Map")*/SceneManager.GetActiveScene().name != "main menu")
            {
                GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
                NetworkServer.Spawn(playerSpawnSystemInstance);
                /*
                GameObject roundSystemInstance = Instantiate(roundSystem);
                NetworkServer.Spawn(roundSystemInstance);*/
            }
        }
        public override void OnServerReady(NetworkConnection conn)
        {
            base.OnServerReady(conn);

            OnServerReadied?.Invoke(conn);
        }
    }
}
