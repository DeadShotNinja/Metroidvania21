using UnityEngine;

namespace Metro
{
    public class SpawnPoint : MonoBehaviour
    {
        [Header("Setup")]
        [Tooltip("Make sure this is unique within each room per spawn point. It can be the same number(s) as in another room.")]
        [SerializeField] private int _spawnID;

        public int SpawnID => _spawnID;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}
