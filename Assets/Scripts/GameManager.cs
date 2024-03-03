using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //variables
    [HideInInspector] public bool isDragging;
    //perhaps this could be more efficient if it was a static variable within 'DragDropToForeground' script
    //but I struggled to get it to work that way

    //for debugging - canvas object appears
    //public GameObject DebugDragText;

    public int objectsClickedOn = 0;

    [SerializeField] GameObject dinoPrefab1;
    [SerializeField] GameObject dinoPrefab2;
    [SerializeField] GameObject dinoPrefab3;
    public GameObject handObject;
    //[SerializeField] Material prefab1Colour;
    //[SerializeField] Material[] dinoColours;

    private float tableXlength = 19;
    private float tableYheight = -1.2f;
    private float tableZmin = 4;
    private float tableZmax = 20;
    private float randomRotation;

    [HideInInspector] public AudioSource gameAudio;

    //public sound clips that are called in other scripts
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip pickedUpSound;


    //experiments with holding original positions (not working)
    //public Vector3 originalPosition;
    //[HideInInspector] public Vector3[] originalPositions = new Vector3[15];


    void Start()
    {

        //getting player audio
        gameAudio = GetComponent<AudioSource>();

        //instantiates 15 dinosaur prefabs
        for (int i = 0; i < 15; i++)
        {

            InstantiateDino();

        }

        //Debug.Log(Random.rotation);



    }

    // Update is called once per frame
    void Update()
    {

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
    private void GenerateRandomRotation()
    {
        randomRotation = Random.Range(0, 1);
    }

    //generates a random number between 0-2, corresponding with prefab number
    private int GenerateRandomArray() 
    {
        //variables
        int number = Random.Range(0, 3);
        return number;
    }
    
    //instantiates a dinosaur in random location within bounds
    private void InstantiateDino()
    {
        //get a random number
        int randomNum = GenerateRandomArray();

        //generate a dinosaur prefab, based on random number
        //N.B - can't seem to do this with an array of game objects; Instantiate doesn't work with them.
        if (randomNum == 0)
        {
            Instantiate(dinoPrefab1, GenerateSpawnPos(), dinoPrefab1.transform.rotation);
        }
        else if (randomNum == 1)
        {
            Instantiate(dinoPrefab2, GenerateSpawnPos(), dinoPrefab2.transform.rotation);
        }
        else if (randomNum == 2)
        {
            Instantiate(dinoPrefab3, GenerateSpawnPos(), dinoPrefab3.transform.rotation);
        }


    }

}
