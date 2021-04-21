using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Mirror.multiplayerOverride
{
    public class HQ : NetworkBehaviour
    {
        // Start is called before the first frame update
        [SyncVar]
        public playerMultiplayerGame mainPlayer;

        [SyncVar/*(hook = nameof(upadteVarColor))*/]
        public Color color;

        [SyncVar/*(hook = nameof(upadteVarID))*/]
        public int ID;

        private networkManagerOverride room;
        private networkManagerOverride Room
        {
            get
            {
                if (room != null) { return room; }
                return room = NetworkManager.singleton as networkManagerOverride;
            }
        }

        public override void OnStartAuthority()
        {
            //Debug.Log("azbbbbbbbbb");
            /*foreach (playerMultiplayerGame player in Room.GamePlayers)
            {
                if(player.hasAuthority)
                {
                    Debug.Log("on client authority");
                    PlayerCMD(player);
                    
                }
            }*/
            GameObject main = GameObject.FindGameObjectWithTag("mainMenu");
            //check if same team
            if (main.GetComponent<buildngPlacement>().selectBox.GetComponent<teamIdentefier>().teamID == gameObject.GetComponent<teamIdentefier>().teamID)
            {
                main.GetComponent<buildngPlacement>().HQ = gameObject;
            }

            Room.GameObjects.Add(gameObject);
            //mainPlayer.UpdateGameObjectColor();

            //updateColCMD();
        }

        public override void OnStartClient()
        {
            UpdateAll();
        }

        /*
        [Command]
        void PlayerCMD(playerMultiplayerGame player)
        {
            Debug.Log(player + " cmd");
            UpdatePlayerMultiplayerGame(player);
        }
        */
        void upadteVarColor(Color oldVal, Color newVal)
        {

            UpdateAll();
        }

        void upadteVarID(int oldVal, int newVal)
        {
            UpdateAll();
        }
        
        public void UpdateAll()
        {
            gameObject.GetComponent<teamIdentefier>().teamID = ID;

            gameObject.GetComponent<teamIdentefier>().teamColor = color;
            gameObject.GetComponent<teamIdentefier>().UpdateColor();
        }


        /*
        [Command]
        void updateColCMD(Color col)
        {
            Debug.Log(col);
            gameObject.GetComponent<teamIdentefier>().teamColor = col;
            gameObject.GetComponent<teamIdentefier>().UpdateColor();
            //UpdateCol(col);
        }
        
        [ClientRpc]
        void UpdateCol(Color col)
        {
            gameObject.GetComponent<teamIdentefier>().teamColor = col;
            gameObject.GetComponent<teamIdentefier>().UpdateColor();
        }
        
        [ClientRpc]
        void UpdatePlayerMultiplayerGame(playerMultiplayerGame player)
        {
            Debug.Log(player + " rpc");
            mainPlayer = player;
        }*/
    }
}