using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSorting_GameManager : MonoBehaviour
{

    private Vector3[] receiverLocations = new Vector3[6];
    [SerializeField] GameObject[] shapeHoles = new GameObject[6];



    // Start is called before the first frame update
    void Start()
    {

        //getting initial positions of each hole
        for (int i = 0; i < 6; i++)
        {
            receiverLocations[i] = shapeHoles[i].transform.position;
        }

        Debug.Log(receiverLocations.Length);

    }

    // Update is called once per frame
    void Update()
    {

        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Randomly swap the positions of the objects
            RandomizeObjectPositions();
        }
    }


    void RandomizeObjectPositions()
    {
        // Shuffle the array of initial positions
        ShuffleArray(receiverLocations);

        // Assign the shuffled positions to the objects
        for (int i = 0; i < shapeHoles.Length; i++)
        {
            shapeHoles[i].transform.position = receiverLocations[i];
        }
    }

    void ShuffleArray(Vector3[] array)
    {
        // Fisher-Yates shuffle algorithm
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Vector3 temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

}
