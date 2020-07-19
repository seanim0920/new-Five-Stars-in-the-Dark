using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTurnCameraRotate : MonoBehaviour
{
    private PlayerControls controls;
    public RectTransform rect;
    public int direction = 0;
    float angle = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rect.rotation = Quaternion.Euler(0, 0, angle);
    }

    public IEnumerator turnLeftCoroutine()
    {
        for (float i = 0; i <= 6; i += 0.09f)
        {
            angle = i;
            yield return new WaitForSeconds(0);
        }
        angle = 6;
    }

    public IEnumerator turnRightCoroutine()
    {
        for (float i = 0; i >= -6; i -= 0.09f)
        {
            angle = i;
            yield return new WaitForSeconds(0);
        }
        angle = -6;
    }

    public void stopTurning()
    {
        StartCoroutine(stopTurningCoroutine());
    }

    private IEnumerator stopTurningCoroutine()
    {
        print("resetting angle");
        for (int i = 0; i <= 50; i++)
        {
            angle *= 0.95f;
            yield return new WaitForSeconds(0);
        }
        angle = 0;
    }
}
