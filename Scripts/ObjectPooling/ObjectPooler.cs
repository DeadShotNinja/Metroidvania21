using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
	public abstract class ObjectPooler<T> : Singleton<ObjectPooler<T>> where T : MonoBehaviour, IPooledObject
    {
        [Header("Setup")]
        [SerializeField] protected List<Pool> m_Pools;

        protected Dictionary<GameObject, Queue<T>> m_PoolDictionary;
        protected HashSet<T> m_ActiveObjects = new HashSet<T>();

        protected override void Awake()
        {
            base.Awake();

            m_PoolDictionary = new Dictionary<GameObject, Queue<T>>();
            foreach (Pool pool in m_Pools)
            {
                CreatePool(pool.Prefab, pool.InitialSize);
            }
        }

        public virtual T GetFromPool(GameObject prefab)
        {
            if (!m_PoolDictionary.ContainsKey(prefab))
            {
                Debug.LogWarning($"Pool for prefab {prefab.name} does not exist.");
                return null;
            }

            if (m_PoolDictionary[prefab].Count == 0)
            {
                InstantiateToPool(prefab, m_PoolDictionary[prefab]);
            }

            T pooledObj = m_PoolDictionary[prefab].Dequeue();
            pooledObj.gameObject.SetActive(true);
            m_ActiveObjects.Add(pooledObj);
            return pooledObj;
        }

        public virtual void ReturnToPool(T objectToReturn)
        {
            objectToReturn.ResetPooledObject();
            objectToReturn.gameObject.SetActive(false);
            m_ActiveObjects.Remove(objectToReturn);
            m_PoolDictionary[objectToReturn.OriginalPrefab].Enqueue(objectToReturn);
        }

        public virtual void ReturnAllToPool()
        {
            List<T> tempList = new List<T>(m_ActiveObjects);

            foreach (T obj in tempList)
            {
                ReturnToPool(obj);
            }

            m_ActiveObjects.Clear();
        }

        protected virtual void CreatePool(GameObject prefab, int initialSize)
        {
            Queue<T> objectPool = new Queue<T>();

            for (int i = 0; i < initialSize; i++)
            {
                InstantiateToPool(prefab, objectPool);
            }

            m_PoolDictionary.Add(prefab, objectPool);
        }

        protected virtual T InstantiateToPool(GameObject prefab, Queue<T> objectPool)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);

            if (obj.TryGetComponent(out T pooledObject))
            {
                pooledObject.OriginalPrefab = prefab;

                objectPool.Enqueue(pooledObject);

                return pooledObject;
            }
            else
            {
                Debug.Log("A projectile prefab was not properly set up (Possibly missing a Projectile component");
                return null;
            }
        }
    }
}