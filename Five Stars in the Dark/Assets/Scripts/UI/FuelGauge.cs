using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelGauge : MonoBehaviour
{
    [SerializeField] private Image needle;
    private float needleAngle;
    private const float minAngle = -65f;
    private const float maxAngle = 30f;
    private float angleRange;
    public float actualAngle;
    // Start is called before the first frame update
    void Start()
    {
        needleAngle = minAngle;
        angleRange = Mathf.Abs(maxAngle - minAngle);
        actualAngle = minAngle;
    }

    // Update is called once per frame
    void Update()
    {
        float anglePercentage = CountdownTimer.getProgress(); //is this expensive?
        float deltaAngle = angleRange * anglePercentage;
        actualAngle = needleAngle - deltaAngle;
        needle.rectTransform.rotation = Quaternion.Euler(0, 0, actualAngle);
    }
}