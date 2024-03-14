using System.Collections.Generic;
using UnityEngine;

public class ColourSorting_ObjectPool : MonoBehaviour
{
    public static ColourSorting_ObjectPool SharedInstance;

    private List<GameObject>[] pooledObjects = new List<GameObject>[9];

    [SerializeField] private GameObject[] objectsToPool;

    private int amountToPool = 5;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        for (int i = 0; i < objectsToPool.Length; i++)
        {
            InstantiatePoolObject(objectsToPool[i]);
        }
    }

    private void InstantiatePoolObject(GameObject objectToPool)
    {
        int index = System.Array.IndexOf(objectsToPool, objectToPool);
        pooledObjects[index] = new List<GameObject>();

        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects[index].Add(tmp);
        }
    }

    public GameObject GetPooledObject(GameObject objectToPool)
    {
        int index = System.Array.IndexOf(objectsToPool, objectToPool);

        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[index][i].activeInHierarchy)
            {
                return pooledObjects[index][i];
            }
        }
        return null;
    }
}