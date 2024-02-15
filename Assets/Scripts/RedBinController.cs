using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBinController : MonoBehaviour
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
        {
            objectRb = other.GetComponent<Rigidbody>();
            objectRb.AddForce(new Vector3(0.5f,1,0) * 18f, ForceMode.Impulse);
        }
        
    }
}