using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mirror.multiplayerOverride
{
    public class multiplayerSpawnSystem : NetworkBehaviour
    {
        [SerializeField] private GameObject playerPrefab = null;

        private static List<Transform> spawnPoints = new List<Transform>();

        private int nextIndex = 0;

        private networkManagerOverride room;
        public networkManagerOverride Room
        {
            get
            {
                if (room != null) { return room; }
                return room = NetworkManager.singleton as networkManagerOverride;
            }
        }

        public static void AddSpawnPoint(Transform transform)
        {
            spawnPoints.Add(transform);

            spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
        }
        public static void RemoveSpawnPoint(Transform transform) => spawnPoints.Remove(transform);

        public override void OnStartServer() => networkManagerOverride.OnServerReadied += SpawnPlayer;
        /*
        public override void OnStartClient()
        {
            InputManager.Add(ActionMapNames.Player);
            InputManager.Controls.Player.Look.Enable();
        }*/

        [ServerCallback]
        private void OnDestroy() => networkManagerOverride.OnServerReadied -= SpawnPlayer;

        [Server]
        public void SpawnPlayer(NetworkConnection conn)
        {
            Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);

            if (spawnPoint == null)
            {
                Debug.LogError($"Missing spawn point for player {nextIndex}");
                return;
            }

            GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
            playerInstance.GetComponent<HQ>().ID = nextIndex;
            playerInstance.GetComponent<HQ>().color = (Room.colors[nextIndex]);
            playerInstance.GetComponent<HQ>().mainPlayer = Room.GamePlayers[nextIndex];
            NetworkServer.Spawn(playerInstance, conn);


            //playerInstance.GetComponent<HQ>().UpdateAll();

            nextIndex++;
        }
    }
}