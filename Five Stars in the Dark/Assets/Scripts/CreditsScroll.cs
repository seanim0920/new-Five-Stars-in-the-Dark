using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public float creditScrollSpeed = 1.0f;

    //Vector3 creditPosition;
    float creditOffsetX;
    float creditOffsetY;

    // Start is called before the first frame update
    void Start()
    {
        //creditPosition = this.gameObject.transform.position;

        creditOffsetX = this.gameObject.transform.position.x;
        creditOffsetY = this.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        creditOffsetY += creditScrollSpeed;
        this.gameObject.transform.position = new Vector2(creditOffsetX, 1 * creditOffsetY);
    }
}
