using UnityEngine;

namespace Metro
{
    /// <summary>
    /// Turns a class into a static instance. Like a Singleton but instead of destroying any new
    /// instances, it overrides the current instance. Used for resetting the state. 
    /// </summary>
    public class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake() => Instance = this as T;

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
}