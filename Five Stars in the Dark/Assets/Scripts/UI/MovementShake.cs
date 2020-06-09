using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementShake : MonoBehaviour
{
    private PlayerControls controls;
    public static float shakeOffset = 5;
    float lerpTime = 0;
    public RectTransform rect;
    Vector2 originalPosition;
    Vector2 modifiedPosition;
    Vector3 originalScale;
    float originalx;
    float originaly;
    void Start()
    {
        controls = GameObject.Find("Player").GetComponent<PlayerControls>();
        originalPosition = rect.anchoredPosition;
        modifiedPosition = originalPosition;
        originalScale = rect.localScale;
        originalx = rect.localPosition.x;
        originaly = rect.localPosition.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 displacement = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector2.right * shakeOffset * (controls.movementSpeed / controls.maxSpeed);
        rect.anchoredPosition = modifiedPosition + displacement;

        modifiedPosition.x = originalx + controls.getStrafeAmount() * 150;
        modifiedPosition.y = originaly + 125*(controls.movementSpeed / controls.maxSpeed);
        rect.localScale = new Vector3(1.0f + (controls.movementSpeed / controls.maxSpeed / 2.25f), 1.0f + (controls.movementSpeed / controls.maxSpeed / 2.25f), 1);
    }
}