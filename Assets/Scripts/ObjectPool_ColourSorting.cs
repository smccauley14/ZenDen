using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_ColourSorting : MonoBehaviour
{
    //variables
    public static ObjectPool_ColourSorting SharedInstance;
    public List<GameObject>[] pooledObjects = new List<GameObject>[9];
    public GameObject[] objectsToPool;
    private int amountToPool = 4;


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

    //instantiate set number of chosen pool object
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
    
    //access one of the objects from pool
    public GameObject GetPooledObject(int poolIndex)
    {

        //if requesting an invalid pool number
        if (poolIndex < 0 || poolIndex >= pooledObjects.Length)
        {
            Debug.LogError("Invalid pool index");
            return null;
        }

        //work through pooledObjects index until you find one that is currently inactive
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

    //TEST - KEEP FOR NOW, AS I MAY WISH TO RETURN TO IT...
    /*
    public void AssignOriginalPosition(Vector3 AssignedPosition, int poolObject, int poolIndex)
    {
        originalPositions[poolObject][poolIndex] = AssignedPosition;
    }

    public void UpdateSpawnPosition(Vector3 newPosition, int poolObject, int poolIndex)
    {
        if (poolObject >= 0 && poolObject < originalPositions.Length &&
            poolIndex >= 0 && poolIndex < originalPositions[poolObject].Count)
        {
            originalPositions[poolObject][poolIndex] = newPosition;
        }
    }
    */

}