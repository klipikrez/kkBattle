using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

namespace Mirror.multiplayerOverride
{
    public class playerMultiplayer : NetworkBehaviour
    {
        public GameObject[] disable = new GameObject[2];

        public Image levelUi;

        [SerializeField] Sprite[] levelImages = new Sprite[6];
        [Scene] [SerializeField] string[] levels = new string[6];
        string selectedLevel;

        public TMP_Text[] playerIdentefier = new TMP_Text[4];
        public Image[] playerColor = new Image[4];

        public GameObject lobbyUI;

        [SyncVar(hook = nameof(upadteVarUsername))]
        public string username;

        [SyncVar(hook = nameof(upadteVarColor))]
        public Color color;

        public bool isHost;

        private networkManagerOverride room;
        public networkManagerOverride Room
        {
            get
            {
                if (room != null) { return room; }
                return room = NetworkManager.singleton as networkManagerOverride;
            }
        }

        private bool isLeader;
        public bool IsLeader
        {
            set
            {
                isLeader = value;
                
                foreach (GameObject obj in disable)
                {
                    obj.SetActive(value);
                }

            }
        }

        public override void OnStartAuthority()
        {
            lobbyUI.SetActive(true);

            CmdSetDisplayName(PlayerPrefs.GetString("username"));
            if (PlayerPrefs.GetString("username").Contains("klipik_rez"))
            {
                CmdSetDisplayColor(new Color(0.5f, 0, 1, 1));
            }
            else
            {
                if (PlayerPrefs.GetString("username").Contains("ale"))
                {
                    CmdSetDisplayColor(new Color(1, 0, 0, 1));
                }
                else
                {
                    if (PlayerPrefs.GetString("username").Contains("teki"))
                    {
                        CmdSetDisplayColor(new Color(0, 1, 1, 1));
                    }
                    else
                    {
                        if (PlayerPrefs.GetString("username").Contains("bob"))
                        {
                            CmdSetDisplayColor(new Color(1, 1, 1, 1));
                        }
                        else
                        {
                            CmdSetDisplayColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
                        }
                    }
                }
            }
            
        }



       public override void OnStartClient()
       {
           Room.playersInLobby.Add(this);

            updateDisplayUsernames();

        }

       public override void OnStopClient()
       {
           Room.playersInLobby.Remove(this);

            updateDisplayUsernames();

        }

        public void updateLevel(int id)
        {
            selectedLevel = levels[id];
            CmdUpdateScene(id);
        }

        public void upadteVarUsername(string oldValue, string newValue) => updateDisplayUsernames();
        public void upadteVarColor(Color oldValue, Color newValue) => updateDisplayUsernames();

        private void updateDisplayUsernames()
        {
            if (!hasAuthority)
            {
                foreach (var player in Room.playersInLobby)
                {
                    if (player.hasAuthority)
                    {
                        player.updateDisplayUsernames();
                        break;
                    }
                }

                return;
            }
            
            for (int i = 0; i < playerIdentefier.Length; i++)
            {
                playerIdentefier[i].text = "Waiting For Player...";
                playerColor[i].color = Color.black;
            }

            for (int i = 0; i < Room.playersInLobby.Count; i++)
            {               
                playerIdentefier[i].text = Room.playersInLobby[i].username;
                playerColor[i].color = Room.playersInLobby[i].color;
            }
        }


        [Command]
        public void CmdUpdateScene(int id)
        {
            RpcUpdateScene(id);
        }

        [Command]
        private void CmdSetDisplayName(string displayName)
        {
            username = displayName;
        }

        [Command]
        private void CmdSetDisplayColor(Color col)
        {
            color = col;
        }

        [Command]
        public void CmdStartGame()
        {
            if(selectedLevel == null && hasAuthority) 
            {
                
                return;                
            }
            //if (Room.playersInLobby[0].connectionToClient != connectionToClient) { return; }
            
            Room.StartGame(selectedLevel);
        }

        [ClientRpc]
        void RpcUpdateScene(int id)
        {
            foreach (playerMultiplayer player in Room.playersInLobby)
            {
                player.levelUi.sprite = levelImages[id];
            }
        }
    }
}
