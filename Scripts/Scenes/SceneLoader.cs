using UnityEngine;
using UnityEngine.SceneManagement;

namespace Metro
{
    public static class SceneLoader
    {
        public static void LoadScene(int sceneIndex)
        {
            if (SceneQueueManager.Instance != null)
            {
                SceneQueueManager.Instance.QueueScene(sceneIndex);
                SceneManager.LoadScene(1);
            }  
            else
            {
                Debug.LogError("No instance of SceneQueueManager found, " +
                    "Scenes will not load!");
            }
        }
    }
}
