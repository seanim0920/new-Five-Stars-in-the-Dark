using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goToEnding : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(goToEndingAfterDelayCoroutine());
    }

    private IEnumerator goToEndingAfterDelayCoroutine()
    {
        print("decision was spawned");
        yield return new WaitForSeconds(10);
        LoadScene.Loader("ExplosionEnding");
    }
}