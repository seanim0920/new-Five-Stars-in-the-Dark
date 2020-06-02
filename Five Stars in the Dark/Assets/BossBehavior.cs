using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fireMissles());
    }

    IEnumerator fireMissles()
    {
        //while true
        yield return new WaitForSeconds(3);
            //instantiate incoming car directed at the player
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
