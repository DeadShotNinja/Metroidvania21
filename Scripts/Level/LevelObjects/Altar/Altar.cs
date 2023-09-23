using UnityEngine;

namespace Metro
{
    public class Altar : MonoBehaviour
    {
        private void OnEnable()
        {
            if (GameDatabase.Instance != null)
                GameDatabase.Instance.GetEnvironmentAudioEvent(EnvironmentAudioType.Play_AlterAmbienceLoop)?.Post(gameObject);
        }

        public void OnInteracted_Altar()
        {
            if (GameDatabase.Instance != null)
                GameDatabase.Instance.GetEnvironmentAudioEvent(EnvironmentAudioType.Play_AlterInteract)?.Post(gameObject);
        }

        private void OnDisable()
        {
            if (GameDatabase.Instance != null)
                GameDatabase.Instance.GetEnvironmentAudioEvent(EnvironmentAudioType.Stop_AlterAmbienceLoop)?.Post(gameObject);
        }
    }
}
