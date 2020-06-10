using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelSelfRotation : MonoBehaviour
{
    public RectTransform wheelImage;
    private float wheelAngle;
    private float speed;
    private float goalAngle;
    private bool right;
    // Start is called before the first frame update
    void Start()
    {
        resetGoalAngle();
        changeWheelImageAngle();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        changeWheelImageAngle();
        wheelImage.rotation = Quaternion.Euler(0, 0, wheelAngle);
    }
    public void changeWheelImageAngle()
    {
        wheelAngle += speed;
        if ((right && wheelAngle >= goalAngle) || (!right && wheelAngle <= goalAngle))
        {
            resetGoalAngle();
        }
    }

    private void resetGoalAngle()
    {
        goalAngle = Random.Range(-10.00f, 45.00f);
        speed = Random.Range(0f, 0.4f);
        right = true;
        if (goalAngle < wheelAngle)
        {
            speed *= -1;
            right = false;
        }
    }
}
