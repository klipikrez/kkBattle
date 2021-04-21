using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mirror.multiplayerOverride
{
    public class mainMultiplayer : MonoBehaviour
    {

        private networkManagerOverride room;
        public networkManagerOverride Room
        {
            get
            {
                if (room != null) { return room; }
                return room = NetworkManager.singleton as networkManagerOverride;
            }
        }

        public GameObject pannel;

        //public GameObject[] playerIdentefier = new GameObject[4];

        public void HostSinglePlayer()
        {
            Room.StartHost();
        }

        public void HostLobby()
        {
            Room.StartHost();

            pannel.GetComponent<Animator>().Play("startHost");
        }

        public void Disconnect()
        {
            Room.StopHost();

            Room.StopClient();
        }



    }
}
