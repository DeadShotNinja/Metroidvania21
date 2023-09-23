namespace Metro
{
    public class SceneQueueManager : PersistantSingleton<SceneQueueManager>
    {
        public int NextSceneToLoad { get; private set; }

        private void Start()
        {
            SceneLoader.LoadScene(2);
        }

        public void QueueScene(int sceneIndex)
        {
            NextSceneToLoad = sceneIndex;
        }
    }
}
