using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    private Rigidbody objectRb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Red"))
        {
            Debug.Log("collision");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Yellow"))
        {   objectRb = other.GetComponent<Rigidbody>();
            objectRb.AddForce(Vector3.right * 2f, ForceMode.Impulse);
        }
    }
}
