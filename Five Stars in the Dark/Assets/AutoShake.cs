using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoShake : MonoBehaviour
{
    public static float shakeOffset = 5;
    float lerpTime = 0;
    public RectTransform rect;
    Vector2 originalPosition;
    Vector2 modifiedPosition;
    Vector3 originalScale;
    float originalx;
    float originaly;
    float movementSpeed = 1; //we can change this to produce auto zoom out or zoom ins
    float maxSpeed = 1;
    float zoomAmount = 1;
    public Image black;
    void Start()
    {
        originalPosition = rect.anchoredPosition;
        modifiedPosition = originalPosition;
        originalScale = rect.localScale;
        originalx = rect.localPosition.x;
        originaly = rect.localPosition.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        zoomAmount *= 0.999f;
        if (!black.enabled)
        {
            originaly -= 0.01f;
        }

        Vector2 displacement = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector2.right * shakeOffset * (movementSpeed / maxSpeed);
        rect.anchoredPosition = modifiedPosition + displacement;

        //modifiedPosition.x = originalx + getStrafeAmount() * 150;
        modifiedPosition.y = originaly + 125 * (movementSpeed / maxSpeed);
        rect.localScale = new Vector3(1.0f + zoomAmount, 1.0f + zoomAmount, 1);
    }
}
