using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
	public abstract class ObjectPooler<T> : Singleton<ObjectPooler<T>> where T : MonoBehaviour, IPooledObject
    {
        [Header("Setup")]
        [SerializeField] protected List<Pool> m_Pools;

        protected Dictionary<GameObject, Queue<T>> m_PoolDictionary;

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
            return pooledObj;
        }

        public virtual void ReturnToPool(T objectToReturn)
        {
            objectToReturn.ResetPooledObject();
            objectToReturn.gameObject.SetActive(false);
            m_PoolDictionary[objectToReturn.OriginalPrefab].Enqueue(objectToReturn);
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