using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace Mirror.multiplayerOverride
{
    public class joinLobby : MonoBehaviour
    {
        [SerializeField] private TMP_InputField ipInputField;

        [SerializeField] private networkManagerOverride networkManager;

        public void connectToIp()
        {
            networkManager.networkAddress = ipInputField.text;
            networkManager.StartClient();
        }

    }
}