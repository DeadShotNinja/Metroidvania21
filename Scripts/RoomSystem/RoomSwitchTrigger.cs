using UnityEngine;

namespace Metro
{
    public class RoomSwitchTrigger : MonoBehaviour
    {
        [Header("Trigger Setup")]
        [SerializeField] private int _targetHolderID;
        [SerializeField] private int _targetSpawnID;

        private RoomVariant _roomVariant;

        public void SetUp(RoomVariant roomVariant)
        {
            _roomVariant = roomVariant;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _roomVariant.OnTransitionTriggered(_targetHolderID, _targetSpawnID);
            }
        }
    }
}
