using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BinController : MonoBehaviour
{
    //variables
    private Rigidbody objectRb;
    private string colour;
    private GameManager gameManager;
    
    //private DragDropToForeground draggingScript;
    //private Transform originalPosition;


    // Start is called before the first frame update
    void Start()
    {
        //get the colour tag of the bin object
        colour = gameObject.tag;
        //Debug.Log(colour);

        //get access to Game Manager script
        gameManager = GameObject.Find("GameManager").GetComponent <GameManager>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if the object is not being dragged (i.e. if it has been dropped)
        if (!gameManager.isDragging)
        {
            //if the object dropped is the same colour as the bin, destroy object
            if (other.CompareTag(colour))
            {
                //Debug.Log("collision");
                Destroy(other.gameObject);
                //play 'correct' sound effect
                gameManager.gameAudio.PlayOneShot(gameManager.correctSound);
            }
            //if the object is a different colour, bounce object vertically
            else if (!other.CompareTag(colour))
            {
                //other.gameObject.transform.position = gameManager.originalPosition;

                //play 'wrong' sound effect
                gameManager.gameAudio.PlayOneShot(gameManager.wrongSound);

                objectRb = other.GetComponent<Rigidbody>();
                objectRb.AddForce(new Vector3(0, 1.2f, 0.10f) * 18f, ForceMode.Impulse);
            }
        }

        //SEE NOTE IN GAME MANAGER
        //perhaps 'isDragging' could be a static variable, rather than within GameManager?

    }


}
