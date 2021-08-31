using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWhenTouched : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //should be adjusted to detect the closest car to the player, if there are multiple cars in the zone
        if (col.gameObject.CompareTag("Car"))
        {
            col.gameObject.GetComponent<NPCMovement>().currentSpeed *= 0.95f;
        }
    }
}
