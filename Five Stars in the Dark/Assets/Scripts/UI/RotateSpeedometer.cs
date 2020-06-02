using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateSpeedometer : MonoBehaviour
{
    [SerializeField] private Image leftNeedle, rightNeedle;
    [SerializeField] private PlayerControls controls;
    private float leftNeedleAngle;
    private float rightNeedleAngle;
    private const float minAngle = 35f;
    private const float maxAngle = -90f;
    private float angleRange;
    public bool isQuickTurning = false;
    public float actualAngle;
    // Start is called before the first frame update
    void Start()
    {
        controls = GameObject.Find("Player").GetComponent<PlayerControls>();
        leftNeedleAngle = minAngle;
        rightNeedleAngle = minAngle;
        angleRange = Mathf.Abs(maxAngle - minAngle);
        actualAngle = minAngle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isQuickTurning)
        {
            float maxSpeedPercentage = controls.movementSpeed / controls.maxSpeed;
            float deltaAngle = angleRange * maxSpeedPercentage;
            actualAngle = leftNeedleAngle - deltaAngle;
            // Map the needle angle to PlayerControls.movementSpeed
            leftNeedle.rectTransform.rotation = Quaternion.Euler(0, 0, actualAngle);
            rightNeedle.rectTransform.rotation = Quaternion.Euler(0, 0, actualAngle);
        }
    }
}
