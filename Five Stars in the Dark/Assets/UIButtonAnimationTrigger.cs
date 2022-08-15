using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonAnimationTrigger : MonoBehaviour
{
    // should this work with a controller too? maybe if pressing a button that isnt mapped to an action?
    Vector3 lastMouseCoordinate = Vector3.zero;
    float lastTimeMoved = 2;
    Animator anim;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lastMouseCoordinate = Input.mousePosition;
        if (!PlaythroughManager.hasPlayedLevel(PlaythroughManager.currentLevelIndex))
        {
            anim.speed = 0; //start buttons at the bottom of the screen if they haven't played this level yet
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;

        //print(mouseDelta.sqrMagnitude);
        // Then we check if it has moved to the left.
        if (!anim.IsInTransition(0))
        {
            if (mouseDelta.sqrMagnitude > 10000)
            {
                lastTimeMoved = Time.time;
                anim.speed = 1;
                anim.CrossFade("PauseUp", 0.6f);
            }
            else
            {
                if (Time.time - lastTimeMoved > 4f)
                {
                    anim.CrossFade("PauseDown", 0.6f);
                }
            }
        }

        // Then we store our mousePosition so that we can check it again next frame.
        lastMouseCoordinate = Input.mousePosition;
    }
}