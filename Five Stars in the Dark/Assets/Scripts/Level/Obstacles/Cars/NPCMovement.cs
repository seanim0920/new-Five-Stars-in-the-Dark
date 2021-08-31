using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    private static GameObject player;
    public AudioSource honk;
    public AudioSource engineSound;
    public float currentSpeed = 0f;
    public float neutralSpeed = 1f;
    public float maxSpeed;
    public float minSpeed;
    private float acceleration = 0.01f;
    private float eyesight = 60;
    private Vector3 movementDirection;
    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(Coast());
    }

    void FixedUpdate()
    {
        //keep refreshing movementdirection, car may rotate
        movementDirection = transform.up;

        transform.position += movementDirection * currentSpeed;
    }

    public GameObject SeesObstacle(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, eyesight);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    public IEnumerator Coast()
    {
        while (currentSpeed < neutralSpeed)
        {
            currentSpeed += acceleration;
            yield return new WaitForFixedUpdate();
        }
        while (currentSpeed > neutralSpeed)
        {
            currentSpeed -= acceleration;
            yield return new WaitForFixedUpdate();
        }
        currentSpeed = neutralSpeed;
        //print(currentSpeed);
    }

    public IEnumerator SwitchLaneRight(bool isRight, float strafeSpeed)
    {
        int direction = 1;
        if (!isRight)
        {
            direction *= -1;
        }

        int laneWidth = 30;
        float endPositionX = transform.position.x + direction * laneWidth;
        while (endPositionX > transform.position.x)
        {
            transform.position += new Vector3(strafeSpeed,0,0);
            yield return new WaitForFixedUpdate();
        }
        while (endPositionX < transform.position.x)
        {
            transform.position -= new Vector3(strafeSpeed, 0, 0);
            yield return new WaitForFixedUpdate();
        }
        transform.position = new Vector3(endPositionX,transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    public IEnumerator speedUp()
    {
        while (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration;
            yield return new WaitForFixedUpdate();
        }
        currentSpeed = maxSpeed;
    }
    public IEnumerator slowDown()
    {
        while (currentSpeed > minSpeed)
        {
            currentSpeed *= 0.98f;
            yield return new WaitForFixedUpdate();
        }
        while (currentSpeed < minSpeed)
        {
            currentSpeed += acceleration/2;
            yield return new WaitForFixedUpdate();
        }
        currentSpeed = minSpeed;
    }
    public IEnumerator suddenStop()
    {
        while (currentSpeed > 0.01f)
        {
            //print("trying to stop car");
            currentSpeed *= 0.96f;
            yield return new WaitForFixedUpdate();
        }
        currentSpeed = 0;
    }
    public void resetMovement()
    {
        StopAllCoroutines();
    }

    public void setSpeed(float speed)
    {
        currentSpeed = speed;
    }

    public static float getRelativeSpeed(GameObject npc)
    {
        return (player.GetComponent<PlayerControls>().currentSpeed - npc.GetComponent<NPCMovement>().currentSpeed * Vector3.Dot(npc.transform.up, player.transform.up));
    }
}
