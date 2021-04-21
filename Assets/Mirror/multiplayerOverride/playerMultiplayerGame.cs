using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

namespace Mirror.multiplayerOverride
{
    public class playerMultiplayerGame : NetworkBehaviour
    {
        [SyncVar]
        private string displayName = "Loading...";

        [SyncVar]
        public Color displayColor = Color.black;

        [SyncVar]
        public int ID;

        public GameObject player;

        private networkManagerOverride room;
        private networkManagerOverride Room
        {
            get
            {
                if (room != null) { return room; }
                return room = NetworkManager.singleton as networkManagerOverride;
            }
        }

        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
        
            if (hasAuthority)
            {
                player = GameObject.FindGameObjectWithTag("selectBox");
                player.GetComponent<teamIdentefier>().teamID = ID;
            }
        }

        public override void OnStartClient()
        {
            DontDestroyOnLoad(gameObject);

            Room.GamePlayers.Add(this);

        }

        public override void OnStopClient()
        {
            Room.GamePlayers.Remove(this);
        }

        [Server]
        public void SetDisplayName(string displayName)
        {
            this.displayName = displayName;
        }
        
        [Server]
        public void SetDisplayColor(Color col)
        {
            this.displayColor = col;
        }

        [Server]
        public void SetID(int id)
        {
            this.ID = id;
        }


        /*
        [Server]
        public void setMainPlayer()
        {
            Room.GameObjects[ID].GetComponent<HQ>().mainPlayer = this;
        }*/

    }
}
