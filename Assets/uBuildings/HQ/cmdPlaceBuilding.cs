using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.multiplayerOverride;

    public class cmdPlaceBuilding : NetworkBehaviour
    {

        GameObject spawnThis;

        GameObject player;

        GameObject building;


        private networkManagerOverride room;
        public networkManagerOverride Room
        {
            get
            {
                if (room != null) { return room; }
                return room = NetworkManager.singleton as networkManagerOverride;
            }
        }

        public void PlaceBuilding(Vector3 pos, Quaternion rot, GameObject pref, GameObject selectBox)
        {
            spawnThis = pref;
            player = selectBox;
            cmdPlace(pos, rot);
            player.GetComponent<selected_dictionary>().SetDestination(building.transform, building);
        }


        [Command]
        void cmdPlace(Vector3 pos, Quaternion rot)
        {

            building = Instantiate(spawnThis, pos, rot);

            building.GetComponent<teamIdentefier>().teamID = player.GetComponent<teamIdentefier>().teamID;
            building.GetComponent<teamIdentefier>().teamColor = player.GetComponent<teamIdentefier>().teamColor;

            
            NetworkServer.Spawn(building, Room.GamePlayers[gameObject.GetComponent<teamIdentefier>().teamID].gameObject);

        }

    }
