using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region singleton
    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (pool pool in pools)
        {
            Queue<GameObject> ObjectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                ObjectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag,ObjectPool);
        }
    }
    public GameObject SpawnFromPool(string tag , Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("error");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
