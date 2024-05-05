using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColourSorting_GameManager : MonoBehaviour
{
    //variables
    [HideInInspector] public bool isDragging;
    [HideInInspector] public ObjectPool_ColourSorting poolScript;
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
    public string trayPositionOfPickedUpObject;
    public bool readyToPickUpAgain = true;

    //public sound clips that are called in other scripts
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip pickedUpSound;

    //UI
    [SerializeField] private Button dinoButton;
    [SerializeField] private Button tractorButton;
    [SerializeField] private GameObject tractorSelected;
    [SerializeField] private GameObject dinoSelected;
    [SerializeField] private Button exitButton;

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
    public int currentlySelectedObjectType = 1;//starts on dinosaurs

    public int currentlySortedLeft;
    public int currentlySortedMiddle;
    public int currentlySortedRight;
    public int sortedObjectToMakeActive;

    private const string sensoryRoomScene = "School Sensory Room";

    // Start is called before the first frame update
    void Start()
    {
        //getting player audio
        gameAudio = GetComponent<AudioSource>();

        //get audioManagers
        audioManagerScript = GameObject.Find("GameManager").GetComponent<ColourSorting_AudioManager>();

        //adding listeners to UI buttons
        dinoButton.onClick.AddListener(DinoSelected);
        tractorButton.onClick.AddListener(TractorSelected);
        exitButton.onClick.AddListener(LoadSensoryRoomScene);

        //getting pooling script
        poolScript = GetComponent<ObjectPool_ColourSorting>();
    }

    // Update is called once per frame
    void Update()
    {
        //activate a new wave when all objects are inactive
        ActivateWaveOfObjects(prefabNumberMinimum, prefabNumberMaximum);
    }

    public void LoadSensoryRoomScene() => SceneManager.LoadScene(sensoryRoomScene);

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
        currentlySelectedObjectType = 1;//for dinosaur
        prefabNumberMinimum = 0;
        prefabNumberMaximum = 3;
        dinoSelected.SetActive(true);
        tractorSelected.SetActive(false);
        DeactivateAllDraggableObjects();
        DeactivateTrayObjects();

        //resetting 'well-done' audio effect counter
        audioManagerScript.wellDoneCounter = 0;
    }

    //if user presses tractor UI button
    private void TractorSelected()
    {
        currentlySelectedObjectType = 2;//for tractor
        prefabNumberMinimum = 6;
        prefabNumberMaximum = 10;
        dinoSelected.SetActive(false);
        tractorSelected.SetActive(true);
        DeactivateAllDraggableObjects();
        DeactivateTrayObjects();

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
            //remove all 'sorted' objects from scene
            DeactivateTrayObjects();

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
        if (trayPositionOfPickedUpObject == "left")
        {
            return 1;
        }
        else if (trayPositionOfPickedUpObject == "middle")
        {
            return 2;
        }
        else if (trayPositionOfPickedUpObject == "right")
        {
            return 3;
        }
        else return 0;
    }

    //ActivateRelevantSortedObject
    public void ActivateRelevantSortedObject()
    {
        //activate relevant sorted object
        if (currentlySelectedObjectType == 1)
        {
            ActivateSortedDino();
        }
        else if (currentlySelectedObjectType == 2)
        {
            ActivateSortedTractor();
        }
    }

    public void ActivateSortedDino()
    {
        int trayPosition = determineWhatObjectIsPickedUp();

        if (trayPosition == 1)
        {
            currentlySortedLeft++;
            ActivateLeftTrayDino();
        }
        else if (trayPosition == 2)
        {
            currentlySortedMiddle++;
            ActivateMiddleTrayDino();
        }
        else if (trayPosition == 3)
        {
            currentlySortedRight++;
            ActivateRightTrayDino();
        }
    }
    public void ActivateSortedTractor()
    {
        int trayPosition = determineWhatObjectIsPickedUp();

        if (trayPosition == 1)
        {
            currentlySortedLeft++;
            ActivateLeftTrayTractor();
        }
        else if (trayPosition == 2)
        {
            currentlySortedMiddle++;
            ActivateMiddleTrayTractor();
        }
        else if (trayPosition == 3)
        {
            currentlySortedRight++;
            ActivateRightTrayTractor();
        }
    }
    void ActivateLeftTrayDino()
    {
        int objectToSort = currentlySortedLeft - 1;
        sortedLeftDinos[objectToSort].SetActive(true);
    }
    void ActivateMiddleTrayDino()
    {
        int objectToSort = currentlySortedMiddle - 1;
        sortedMiddleDinos[objectToSort].SetActive(true);
    }
    void ActivateRightTrayDino()
    {
        int objectToSort = currentlySortedRight - 1;
        sortedRightDinos[objectToSort].SetActive(true);
    }
    void ActivateLeftTrayTractor()
    {
        int objectToSort = currentlySortedLeft - 1;
        sortedLeftTractors[objectToSort].SetActive(true);
    }
    void ActivateMiddleTrayTractor()
    {
        int objectToSort = currentlySortedMiddle - 1;
        sortedMiddleTractors[objectToSort].SetActive(true);
    }
    void ActivateRightTrayTractor()
    {
        int objectToSort = currentlySortedRight - 1;
        sortedRightTractors[objectToSort].SetActive(true);
    }

    //method to deactivate every draggable object in the scene
    private void DeactivateAllDraggableObjects()
    {
        DragDropToForeground[] allObjects = FindObjectsOfType<DragDropToForeground>();
        foreach (DragDropToForeground singleObject in allObjects)
        {
            singleObject.gameObject.SetActive(false);
        }

        //reduce number objects to 0, to trigger another wave
        objectsInScene = 0;
    }

    private void DeactivateTrayObjects()
    {
        for (int i = 0; i < 4; i++)
        {
            sortedLeftDinos[i].SetActive(false);
            sortedMiddleDinos[i].SetActive(false);
            sortedRightDinos[i].SetActive(false);
            sortedLeftTractors[i].SetActive(false);
            sortedMiddleTractors[i].SetActive(false);
            sortedRightTractors[i].SetActive(false);
        }
        currentlySortedLeft = 0;
        currentlySortedRight = 0;
        currentlySortedMiddle = 0;
    }

}
