using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mirror.multiplayerOverride
{
    public class multiplayerSpawnPoint : MonoBehaviour
    {
        private void Awake() => multiplayerSpawnSystem.AddSpawnPoint(transform);
        private void OnDestroy() => multiplayerSpawnSystem.RemoveSpawnPoint(transform);

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 10f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 15);
        }
    }
}
