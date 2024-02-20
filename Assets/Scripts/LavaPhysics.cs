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
    public float maxHeight = 2.0f;
    public float minHeight = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        //lava balls rise when hot, cool, then fall. Heatsource heats balls, cooling proportioanl to a passive constant plus an increased amount proportional temperature.
    }


    private void FixedUpdate()
    {
        

        //distance to heat source
        heatDistance = Mathf.Abs(transform.position.y - heatSourceY);

        //setting movement proportional to gravity and bouyancy
        velocity = new Vector3(0, (bouyancy-gravity)*Time.deltaTime, 0);
        transform.localPosition += (velocity * Time.deltaTime);

        //determining rate of cooling of lava
        passiveCooling = 0.05f + temperature * coolingConstant;

        //setting lava ball height restrictions
        if (transform.position.y < 0)
        {
        transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
        }

        if (transform.position.y > 2)
        {
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }

        //temperature proportional to inverse square of distance to heat source
        temperature += (heatInfluence * (1 / heatDistance) * (1 / heatDistance)) - passiveCooling;

        //bouyancy influenced by temperature
        bouyancy += bouyancyConversion * temperature;
    }
}
