using UnityEngine;

namespace Metro
{
    /// <summary>
    /// Transforms the static instance into a basic singleton. Destroys any newly
    /// created version, leaving the original. 
    /// </summary>
    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            base.Awake();
        }
    }
}