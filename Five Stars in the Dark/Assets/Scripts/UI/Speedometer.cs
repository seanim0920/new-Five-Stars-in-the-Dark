using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Image needle;
    private PlayerControls controls;
    private float needleAngle;
    private const float minAngle = 35f;
    private const float maxAngle = -90f;
    private float angleRange;
    public bool isQuickTurning = false;
    public float actualAngle;
    // Start is called before the first frame update
    void Start()
    {
        controls = GameObject.Find("Player").GetComponent<PlayerControls>();
        needleAngle = minAngle;
        angleRange = Mathf.Abs(maxAngle - minAngle);
        actualAngle = minAngle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isQuickTurning)
        {
            float maxSpeedPercentage = controls.currentSpeed / controls.maxSpeed;
            float deltaAngle = angleRange * maxSpeedPercentage;
            actualAngle = needleAngle - deltaAngle;
            // Map the needle angle to PlayerControls.movementSpeed
            needle.rectTransform.rotation = Quaternion.Euler(0, 0, actualAngle);
        }
    }
}
