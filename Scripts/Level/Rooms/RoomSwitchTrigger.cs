using System;
using UnityEngine;

namespace Metro
{
    public class RoomSwitchTrigger : MonoBehaviour
    {
        [Header("Trigger Setup")]
        [SerializeField] private int _targetRoomID;
        [SerializeField] private int _targetSpawnID;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                //RoomSwitchTriggerAction?.Invoke(_targetRoomID, _targetSpawnID);
                EventManager.TriggerEvent(new ChangeRoomEvent(_targetRoomID, _targetSpawnID));
            }
        }
    }
}
