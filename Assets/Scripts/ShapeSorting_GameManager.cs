using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSorting_GameManager : MonoBehaviour
{

    private Vector3[] receiverLocations = new Vector3[6];
    private Vector3[] shapeLocations = new Vector3[6];
    [SerializeField] private GameObject[] shapeHoles = new GameObject[6];
    [SerializeField] private GameObject[] shapeObjects = new GameObject[6];
    private Rigidbody[] objectRBs = new Rigidbody[6];

    [HideInInspector] public int shapesInScene = 6;
    private int waveCount;

    public bool shapeReturning = false;
    public int shapeToReturn;


    // Start is called before the first frame update
    void Start()
    {

        GetStartingPositionsOfHoles();

        GetStartingPositionsOfShapes();

        GetRigidBodies();

    }

    // Update is called once per frame
    void Update()
    {

        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Randomly swap the positions of the objects
            RandomizeHolePositions();
        }


        //new wave of objects
        if (shapesInScene <1)
        {
            RespawnShapesInNewPositions();
            shapesInScene = 6;
        }


        if (shapeReturning == true)
        {
            ReturnShapeToOrigPosition(shapeToReturn);
        }

    }

    void ReturnShapeToOrigPosition(int shapeNum)
    {
        shapeObjects[shapeNum].transform.position = shapeLocations[shapeNum];
        shapeReturning = false;

        RemoveRBEffects(shapeNum);

    }


    void RespawnShapesInNewPositions()
    {
        RandomizeHolePositions();
        RandomizeShapePositions();

        for (int i = 0; i < 6; i++)
        {
            shapeObjects[i].SetActive(true);
            RemoveRBEffects(i);
        }
    }

    
    private void RemoveRBEffects(int shapeNum)
    {
        //removing any RB physics effects from previous interactions

        objectRBs[shapeNum].velocity = new Vector3(0, 0, 0);
        objectRBs[shapeNum].angularVelocity = new Vector3(0, 0, 0);
    }
    

    void GetStartingPositionsOfHoles()
    {
        //getting initial positions of each hole
        for (int i = 0; i < 6; i++)
        {
            receiverLocations[i] = shapeHoles[i].transform.position;
        }
    }

    void GetStartingPositionsOfShapes()
    {
        //getting initial positions of each hole
        for (int i = 0; i < 6; i++)
        {
            shapeLocations[i] = shapeObjects[i].transform.position;
        }
    }

    //get RBs of all shapes
    void GetRigidBodies()
    {
        //getting RBs of each shape
        for (int i = 0; i < 6; i++)
        {
            objectRBs[i] = shapeObjects[i].GetComponent<Rigidbody>();
        }
    }


    void RandomizeHolePositions()
    {
        // Shuffle the array of initial positions
        ShuffleArray(receiverLocations);

        // Assign the shuffled positions to the objects
        for (int i = 0; i < shapeHoles.Length; i++)
        {
            shapeHoles[i].transform.position = receiverLocations[i];
        }
    }

    void RandomizeShapePositions()
    {
        // Shuffle the array of initial positions
        ShuffleArray(shapeLocations);

        // Assign the shuffled positions to the objects
        for (int i = 0; i < shapeObjects.Length; i++)
        {
            shapeObjects[i].transform.position = shapeLocations[i];
        }
    }

    void ShuffleArray(Vector3[] array)
    {
        //shuffle algorithm
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Vector3 temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }



}
