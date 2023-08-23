using System;
using UnityEngine;

namespace Metro
{
    public class RoomSwitchTrigger : MonoBehaviour
    {
        [Header("Trigger Setup")]
        [SerializeField] private int _targetHolderID;
        [SerializeField] private int _targetSpawnID;

        public event Action<int, int> RoomSwitchTriggerAction;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                RoomSwitchTriggerAction?.Invoke(_targetHolderID, _targetSpawnID);
            }
        }
    }
}
