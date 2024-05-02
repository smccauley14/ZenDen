using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ColourSorting_GameManager : MonoBehaviour
{
    //variables
    [HideInInspector] public bool isDragging;
    [HideInInspector] public ObjectPool_ColourSorting poolScript;

    //being added/substracted from to by DragDropToForeground script
    [HideInInspector] public int objectsClickedOn = 0;

    [HideInInspector] public bool UIisActive;//true if UI menu is activated by colour selector script
    [SerializeField] private GameObject[] objectPrefabs;
    public GameObject handObject;

    [HideInInspector] public int objectsInScene;
    private float tableXlength = 19;
    private float tableYheight = -1.2f;
    private float tableZmin = 4;
    private float tableZmax = 20;
    private int prefabNumberMinimum = 0;
    private int prefabNumberMaximum = 3;
    [HideInInspector] public AudioSource gameAudio;
    private ColourSorting_AudioManager audioManagerScript;

    //NEW
    public string TrayPositionOfPickedUpObject;

    //public sound clips that are called in other scripts
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip pickedUpSound;

    //UI
    [SerializeField] private Button dinoButton;
    [SerializeField] private Button tractorButton;
    [SerializeField] private GameObject tractorSelected;
    [SerializeField] private GameObject dinoSelected;

    //for managing colour changing functionality
    public Color[] allColours;
    [SerializeField] private Material[] prefabMaterials;

    //Sorted GameObjects: Dinos & Tractors
    public GameObject[] sortedLeftDinos;
    public GameObject[] sortedMiddleDinos;
    public GameObject[] sortedRightDinos;
    public GameObject[] sortedLeftTractors;
    public GameObject[] sortedMiddleTractors;
    public GameObject[] sortedRightTractors;

    public int currentlySortedLeft;
    public int currentlySortedMiddle;
    public int currentlySortedRight;


    // Start is called before the first frame update
    void Start()
    {
        //getting player audio
        gameAudio = GetComponent<AudioSource>();

        //get audioManager
        audioManagerScript = GameObject.Find("GameManager").GetComponent<ColourSorting_AudioManager>();

        //adding listeners to UI buttons
        dinoButton.onClick.AddListener(DinoSelected);
        tractorButton.onClick.AddListener(TractorSelected);

        //getting pooling script
        poolScript = GetComponent<ObjectPool_ColourSorting>();
    }

    // Update is called once per frame
    void Update()
    {
        //activate a new wave when all objects are inactive
        ActivateWaveOfObjects(prefabNumberMinimum, prefabNumberMaximum);
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

    //if user presses dinosaur UI button
    private void DinoSelected()
    {
        prefabNumberMinimum = 0;
        prefabNumberMaximum = 3;
        dinoSelected.SetActive(true);
        tractorSelected.SetActive(false);
        DeactivateAllObjects();

        //resetting 'well-done' audio effect counter
        audioManagerScript.wellDoneCounter = 0;
    }

    //if user presses tractor UI button
    private void TractorSelected()
    {
        prefabNumberMinimum = 6;
        prefabNumberMaximum = 10;
        dinoSelected.SetActive(false);
        tractorSelected.SetActive(true);
        DeactivateAllObjects();

        //resetting 'well-done' audio effect counter
        audioManagerScript.wellDoneCounter = 0;
    }

    //generates a random number, corresponding with prefab number
    private int GenerateRandomArray()
    {
        int number = Random.Range(prefabNumberMinimum, prefabNumberMaximum);
        return number;
    }


    //activating a single pool object
    private void ActivateOnePoolObject(int objectNumber)
    {

        //variables
        Vector3 spawnPos = GenerateSpawnPos();
        Quaternion spawnRotation = GenerateRandomRotation();

        //assign one pool object to GameObject variable
        GameObject draggableObject = ObjectPool_ColourSorting.SharedInstance.GetPooledObject(objectNumber);

        draggableObject.transform.position = spawnPos;
        draggableObject.transform.rotation = spawnRotation;

        draggableObject.SetActive(true);

    }

    //nested for loop to activate a full wave of objects
    private void ActivateWaveOfObjects(int min, int max)
    {

        //specifying the quantity of each separate prefab
        int numberOfEachPrefab = 4;

        //if all objects are deactivated, activate a new wave
        if (objectsInScene < 1)
        {
            //resetting 'well-done' audio effect counter
            audioManagerScript.wellDoneCounter = 0;

            //giving instruction at start of wave
            audioManagerScript.GiveUserVerbalDirectionsAtBeginningOfGame();

            //activating 12 objects - 4 of each colour
            for (int i = min; i < max; i++)
            {
                for (int j = 0; j < numberOfEachPrefab; j++)
                {
                    ActivateOnePoolObject(i);
                    objectsInScene++;
                }
            }
        }



    }

    public int determineWhatObjectIsPickedUp()
    {
        if (TrayPositionOfPickedUpObject == "left")
        {
            return 1;
        }
        else if (TrayPositionOfPickedUpObject == "middle")
        {
            return 2;
        }
        else if (TrayPositionOfPickedUpObject == "right")
        {
            return 3;
        }
        else return 0;
    }


    //ActivateRelevantSortedObject
    public void ActivateSortedObject()
    {
        int trayPosition = determineWhatObjectIsPickedUp();

        if (trayPosition == 1)
        {
            currentlySortedLeft++;
            ActivateLeftTrayObject();
        }
        else if (trayPosition == 2)
        {
            currentlySortedMiddle++;
            ActivateMiddleTrayObject();
        }
        else if (trayPosition == 3)
        {
            currentlySortedRight++;
            ActivateRightTrayObject();
        }
    }
    void ActivateLeftTrayObject()
    {
        int objectToSort = currentlySortedLeft - 1;
        sortedLeftDinos[objectToSort].SetActive(true);
    }
    void ActivateMiddleTrayObject()
    {
        int objectToSort = currentlySortedMiddle - 1;
        sortedMiddleDinos[objectToSort].SetActive(true);
    }
    void ActivateRightTrayObject()
    {
        int objectToSort = currentlySortedRight - 1;
        sortedRightDinos[objectToSort].SetActive(true);
    }

    //method to deactivate every draggable object in the scene
    private void DeactivateAllObjects()
    {
        DragDropToForeground[] allObjects = FindObjectsOfType<DragDropToForeground>();
        foreach (DragDropToForeground singleObject in allObjects)
        {
            singleObject.gameObject.SetActive(false);
        }

        //reduce number objects to 0, to trigger another wave
        objectsInScene = 0;
    }

}
