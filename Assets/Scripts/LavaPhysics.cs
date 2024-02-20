using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPhysics : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float gravity = 0.1f;
    public float temperature = 0.0f;
    public float heatInfluence = 1.0f;
    public float passiveCooling;
    public float bouyancy;
    public float heatSourceY = -0.5f;
    public float heatDistance;
    public float bouyancyConversion = 0.01f;
    public float coolingConstant = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void FixedUpdate()
    {
        //don't fall below heat source
        // if (transform.position.y < heatSourceY)
        //   {
        //     transform.position = new Vector3(transform.position.x, heatSourceY, transform.position.z);
        //  }

        //distance to heat source
        heatDistance = Mathf.Abs(transform.position.y - heatSourceY);

        //heating of the game object
        // temperature += ((heatInfluence * (1/heatDistance)) - passiveCooling);


        //impact of heat on velocity
        //  velocity += new Vector3 (0,(temperature * bouyancy) + gravity,0);

        //velocity movement
        // transform.localPosition += (velocity * Time.deltaTime);

        velocity = new Vector3(0, (bouyancy-gravity)*Time.deltaTime, 0);
        transform.localPosition += (velocity * Time.deltaTime);
        passiveCooling = 0.05f + temperature * coolingConstant;

        if (transform.position.y < 0.1)
        {
        transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        }

        temperature += (heatInfluence * (1 / heatDistance) * (1 / heatDistance)) - passiveCooling;

        bouyancy += bouyancyConversion * temperature;
    }
}
