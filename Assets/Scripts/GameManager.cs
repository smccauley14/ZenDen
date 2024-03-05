using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //variables
    [HideInInspector] public bool isDragging;

    //being added/substracted from to by DragDropToForeground script
    public int objectsClickedOn = 0;

    [SerializeField] GameObject[] dinoPrefabs;

    /*
    [SerializeField] GameObject dinoPrefab1;
    [SerializeField] GameObject dinoPrefab2;
    [SerializeField] GameObject dinoPrefab3;
    */
    public GameObject handObject;

    //[SerializeField] Material prefab1Colour;
    //[SerializeField] Material[] dinoColours;

    public int dinosInScene;
    private float tableXlength = 19;
    private float tableYheight = -1.2f;
    private float tableZmin = 4;
    private float tableZmax = 20;

    [HideInInspector] public AudioSource gameAudio;

    //public sound clips that are called in other scripts
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip pickedUpSound;



    void Start()
    {
        //getting player audio
        gameAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        InstantiateDinos();
    }



    //generates a random position within bounds to spawn dino prefab
    public Vector3 GenerateSpawnPos()
    {
        //variables
        float spawnPosX = Random.Range(-tableXlength, tableXlength);
        float spawnPosZ = Random.Range(tableZmin, tableZmax);
        Vector3 spawnPos = new Vector3(spawnPosX, tableYheight, spawnPosZ);

        return spawnPos;
    }

    //experiment to give objects a randomised rotation...
    private Quaternion GenerateRandomRotation()
    {
        Quaternion facingLeft = Quaternion.Euler(-90f, 180f, 0f);
        Quaternion facingRight = Quaternion.Euler(-90f, 0f, 0f);

        int randomNum = Random.Range(0, 2);

        if (randomNum == 0)
        {
            return facingLeft;
        }
        else
        {
            return facingRight;
        }
    }

    //generates a random number between 0-2, corresponding with prefab number
    private int GenerateRandomArray() 
    {
        //variables
        int number = Random.Range(0, 6);
        return number;
    }
    
    //instantiates a dinosaur in random location within bounds
    private void InstantiateDino()
    {
        //get a random number
        int randomNum = GenerateRandomArray();

        //generate a dinosaur prefab, based on random number
        if (randomNum == 0)
        {
            Instantiate(dinoPrefabs[0], GenerateSpawnPos(), GenerateRandomRotation());
        }
        else if (randomNum == 1)
        {
            Instantiate(dinoPrefabs[1], GenerateSpawnPos(), GenerateRandomRotation());
        }
        else if (randomNum == 2)
        {
            Instantiate(dinoPrefabs[2], GenerateSpawnPos(), GenerateRandomRotation());
        }
        if (randomNum == 3)
        {
            Instantiate(dinoPrefabs[3], GenerateSpawnPos(), GenerateRandomRotation());
        }
        else if (randomNum == 4)
        {
            Instantiate(dinoPrefabs[4], GenerateSpawnPos(), GenerateRandomRotation());
        }
        else if (randomNum == 5)
        {
            Instantiate(dinoPrefabs[5], GenerateSpawnPos(), GenerateRandomRotation());
        }
    }

    private void InstantiateDinos()
    {
        if (dinosInScene < 1)
        {
            for (int i = 0; i < 15; i++)
            {
                InstantiateDino();
                dinosInScene++;
            }
        }
    }

}
