using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ColourSorting_GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //variables
    [HideInInspector] public bool isDragging;

    [HideInInspector] public ObjectPool_ColourSorting poolScript;

    //being added/substracted from to by DragDropToForeground script
    [HideInInspector] public int objectsClickedOn = 0;

    [SerializeField] GameObject[] dinoPrefabs;

    public GameObject handObject;

    //[SerializeField] Material prefab1Colour;
    //[SerializeField] Material[] dinoColours;

    //[HideInInspector]
    public int dinosInScene;
    private float tableXlength = 19;
    private float tableYheight = -1.2f;
    private float tableZmin = 4;
    private float tableZmax = 20;

    private int prefabNumberMinimum = 0;
    private int prefabNumberMaximum = 3;

    [HideInInspector] public AudioSource gameAudio;

    //public sound clips that are called in other scripts
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip pickedUpSound;

    //UI
    [SerializeField] private Button dinoButton;
    [SerializeField] private Button tractorButton;
    [SerializeField] private GameObject tractorSelected;
    [SerializeField] private GameObject dinoSelected;



    void Start()
    {
        //getting player audio
        gameAudio = GetComponent<AudioSource>();

        dinoButton.onClick.AddListener(DinoSelected);
        tractorButton.onClick.AddListener(TractorSelected);


        poolScript = GetComponent<ObjectPool_ColourSorting>();




    }


    // Update is called once per frame
    void Update()
    {



        //InstantiateObjects();


        //TEST
        if (dinosInScene < 1)
        {
            ActivateWaveOfObjects(prefabNumberMinimum, prefabNumberMaximum);
        }


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

    //Randomly choose whether to rotate right or left
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


    private void DinoSelected()
    {
        prefabNumberMinimum = 0;
        prefabNumberMaximum = 3;
        dinoSelected.SetActive(true);
        tractorSelected.SetActive(false);
        //DestroyAllObjects();
        DeactivateAllObjects();
    }

    private void TractorSelected()
    {
        prefabNumberMinimum = 6;
        prefabNumberMaximum = 10;
        dinoSelected.SetActive(false);
        tractorSelected.SetActive(true);
        //DestroyAllObjects();
        DeactivateAllObjects();
    }

    //generates a random number, corresponding with prefab number
    private int GenerateRandomArray()
    {
        int number = Random.Range(prefabNumberMinimum, prefabNumberMaximum);
        return number;
    }

    //instantiates a single dinosaur in random location within bounds
    private void InstantiateOneObject()
    {
        //get a random number
        int randomNum = GenerateRandomArray();

        Instantiate(dinoPrefabs[randomNum], GenerateSpawnPos(), GenerateRandomRotation());
    }


    private void ActivateOnePoolObject(int objectNumber)
    {

        //get a random number
        //int randomNum = GenerateRandomArray();

        GameObject draggableObject = ObjectPool_ColourSorting.SharedInstance.GetPooledObject(objectNumber);

        draggableObject.transform.position = GenerateSpawnPos();
        draggableObject.transform.rotation = GenerateRandomRotation();
        draggableObject.SetActive(true);

    }

    private void ActivateWaveOfObjects(int min, int max)
    {
            //activating 15 objects - 5 of each colour
            for (int i = min; i < max; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    ActivateOnePoolObject(i);
                    dinosInScene++;
            }

                //ActivateOnePoolObject(i);
                //dinosInScene++;
                //Debug.Log("Success");

            }
        
    }

    //instantiate 15 objects (i.e. a wave)
    private void InstantiateObjects()
    {
        if (dinosInScene < 1)
        {
            for (int i = 0; i < 15; i++)
            {
                InstantiateOneObject();
                dinosInScene++;
            }
        }
    }

    //method to get rid of every draggable object in the scene
    private void DestroyAllObjects()
    {
        DragDropToForeground[] allObjects = FindObjectsOfType<DragDropToForeground>();
        foreach (DragDropToForeground singleObject in allObjects)
        {
            Destroy(singleObject.gameObject);
        }

        //reduce number objects to 0, to trigger another wave
        dinosInScene = 0;

    }

    //TEST METHOD
    private void DeactivateAllObjects()
    {
        DragDropToForeground[] allObjects = FindObjectsOfType<DragDropToForeground>();
        foreach (DragDropToForeground singleObject in allObjects)
        {
            singleObject.gameObject.SetActive(false);
        }

        //reduce number objects to 0, to trigger another wave
        dinosInScene = 0;

    }


}
