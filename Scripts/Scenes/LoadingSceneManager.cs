using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Metro
{
    public class LoadingSceneManager : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private Slider _loadingBar;

        private void Start()
        {
            if (SceneQueueManager.Instance == null)
            {
                Debug.LogError("SceneQueueManager not found, can't load scenes", this);
                return;
            }

            StartCoroutine(LoadSceneAsync());
        }

        private IEnumerator LoadSceneAsync()
        {
            int sceneIndex = SceneQueueManager.Instance.NextSceneToLoad;

            if (sceneIndex < 0 || sceneIndex > 4)
            {
                Debug.LogError("Scene index is possibly out of range", this);
                yield break;
            }

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                _loadingBar.value = progress;
                yield return null;
            }
        }
    }
}
