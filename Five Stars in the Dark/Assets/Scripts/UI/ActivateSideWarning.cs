using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateSideWarning : MonoBehaviour
{
    [SerializeField]
    private Sprite on;
    [SerializeField]
    private Sprite off;
    private GameObject left;
    private GameObject right;

    private float maxDistance = 9;
    // Start is called before the first frame update
    void Start()
    {
        string uiPath = "/Main Camera/MainCanvas/DriverView/";
        left = GameObject.Find(uiPath + "LeftWarning");
        right = GameObject.Find(uiPath + "RightWarning");
    }

    // Update is called once per frame
    void Update()
    {
        if(left != null)
        {
            RaycastHit2D leftEarBack = Physics2D.Raycast(transform.position - new Vector3(0,0.5f,0), -transform.right, maxDistance);
            RaycastHit2D leftEarFront = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), -transform.right, maxDistance);
            if(leftEarBack.collider != null && leftEarBack.collider.gameObject.tag != "Zone" ||
            leftEarFront.collider != null && leftEarFront.collider.gameObject.tag != "Zone")
            {
                left.SetActive(true);
            }
            else
            {
                left.SetActive(false);
            }
        }

        if(right != null)
        {
            RaycastHit2D rightEarBack = Physics2D.Raycast(transform.position - new Vector3(0, 0.5f, 0), transform.right, maxDistance);
            RaycastHit2D rightEarFront = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.right, maxDistance);
            if(rightEarBack.collider != null && rightEarBack.collider.gameObject.tag != "Zone" ||
            rightEarFront.collider != null && rightEarFront.collider.gameObject.tag != "Zone")
            {
                right.SetActive(true);
            }
            else
            {
                right.SetActive(false);
            }
        }
    }
}
