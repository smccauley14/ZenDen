using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_ColourSorting : MonoBehaviour
{
    //variables
    public static ObjectPool_ColourSorting SharedInstance;
    public List<GameObject>[] pooledObjects = new List<GameObject>[9];
    public GameObject[] objectsToPool;
    private int amountToPool = 5;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        //instantiate all of the pool objects
        for (int i = 0; i < objectsToPool.Length; i++)
        {
            InstantiatePoolObject(objectsToPool[i]);
        }
    }

    //instantiate 5 of chosen pool object
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

    public GameObject GetPooledObject(int poolIndex)
    {


        if (poolIndex < 0 || poolIndex >= pooledObjects.Length)
        {
            Debug.LogError("Invalid pool index");
            return null;
        }

        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[poolIndex][i].activeInHierarchy)
            {
                return pooledObjects[poolIndex][i];
            }
        }

        
        // If no inactive objects found, instantiate a new one and add it to the pool
        GameObject newObj = Instantiate(objectsToPool[poolIndex]);
        newObj.SetActive(false);
        pooledObjects[poolIndex].Add(newObj);
        return newObj;
        
    }


}