using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    //sound effects and haptic feedback
    public AudioSource releaseAccelPedalSound;
    public AudioSource releaseBrakePedalSound;
    public AudioSource pressAccelPedalSound;
    public AudioSource pressBrakePedalSound;
    public AudioSource disabledWheelSound;
    public AudioSource engineSound;
    public AudioSource rollSound;
    public AudioSource slidingSound;
    public AudioSource wheelGrabSound;
    public SteeringWheelInput wheelFunctions;

    public AudioSource strafeSound;

    public AudioMixer engineMixer;
    public AudioMixerSnapshot[] engineSounds;
    public AudioMixerSnapshot shutOffSound;

    //car speed stats
    public float minSpeed = 0f;
    public float maxSpeed = 1.5f;
    public float neutralSpeed = 1f;
    public float acceleration = 0.01f;
    public float currentSpeed = 0f;

    private float accelAmount = 0;
    private float breakAmount = 0;
    private float strafeAmount = 0;

    private Rigidbody2D body;
    private Vector3 movementDirection;
    private int blockedSide = 0;
    private float lastRecordedStrafe = 0;
    private int strafingDirection = -1;

    private bool acceling = false;
    private bool braking = false;
    private bool steeringRight = false;
    private bool steeringLeft = false;
    private int steerDirection = 0;

    [Header("Private Attributes (visible for debugging)")]
    public float[] snapshotWeights;

    void Start()
    {
        // engineSounds = engineSound.transform;
        body = GetComponent<Rigidbody2D>();
        movementDirection = transform.up;

        // AudioMixerSnapshot[] engineSounds = {restToCoast, coastToAccel};
    }

    void FixedUpdate()
    {
        //balance the ambient audio sfx
        engineSound.volume = -Mathf.Pow((currentSpeed / maxSpeed), 2) + 1;
        rollSound.volume = Mathf.Pow((currentSpeed / maxSpeed), 2);
        strafeSound.volume = rollSound.volume / 2;
        //wheelFunctions.PlayDirtRoadForce((int)(Mathf.Pow((1-(currentSpeed/maxSpeed)),1) * 25));

        //move the car
        transform.position += movementDirection * currentSpeed;

        if (acceling)
        {
            speedUp(accelAmount);
            if (accelAmount < 1f)
            {
                accelAmount += 0.02f;
            }
        }
        else if (braking) //means we cant hold brake and accel at the same time
        {
            slowDown(breakAmount);
            strafeAmount *= 0.92f;
            breakAmount = 0.02f;
        }
        else
        {
            coast();
        }

        if (steerDirection == 1)
        {
            strafeAmount += 0.01f;
        }
        else if (steerDirection == -1)
        {
            strafeAmount -= 0.01f;
        }

        strafe(strafeAmount); //2.08f normalizes strafeamount
        strafeAmount *= 0.97f;
        if (Mathf.Abs(strafeAmount) < 0.01f && slidingSound.isPlaying) slidingSound.Stop();
        accelAmount *= 0.97f;

        /*
         * if not accelerating and not braking
         * coast()
         * what happens if you brake and accel at the same time?
         * have counters for digital strafing, accelerating and braking. the counters either increase every time the functions are called, or we only listen for keydown/keyup and increase the counters, then stop increasing when keyup/keydown. but then we'd need to debounce in the latter method maybe. 
         * would take less code in the controller scripts if they didnt have to worry about counters.
         * 
         * if (!acceling && !braking) {
         * 
         * }
         * 
         * use keyup and keydown to toggle acceling and braking flags
         * put control logic in here. only have keyboardcontrol and gamepadcontrol fire events onkeydown or onkeyup, on player input, not while doing nothing. should eliminate bugs. only potential bugs are if both gamepad and keyboard are used at once
         * 
         * 
        controlFunctions.strafe(strafeVelocity);

        if (!isStrafing) {
            strafeVelocity *= 0.97f;
        }
         */
    }

    public void StartSteer(bool steerRight)
    {
        print("Start steer fired" + steerRight);
        if (!this.enabled)
        {
            StartCoroutine(turnFailCoroutine(steerRight));
            return;
        } else if (steerRight)
        {
            print("Start steering right");
            steerDirection = 1;
            steeringRight = true;
        } else
        {
            print("Start steering left");
            steerDirection = -1;
            steeringLeft = true;
        }

        if (slidingSound.isPlaying)
        {
            //wheelGrabSound.Play();
            slidingSound.Stop();
        }
    }
    public void EndSteer(bool steerRight)
    {
        print("End steer fired" + steerRight);
        if (steerRight)
        {
            steeringRight = false;
        }
        else
        {
            steeringLeft = false;
        }

        if (steeringRight)
        {
            steerDirection = 1;
        } else if (steeringLeft)
        {
            steerDirection = -1;
        } else
        {
            if (!slidingSound.isPlaying)
            {
                slidingSound.panStereo = steerDirection;
                slidingSound.Play();
            }
            print("Stop steering");
            steerDirection = 0;
        }
    }

    public void SteerLeft()
    {
        strafeAmount -= 0.01f;
    }

    public void SteerRight()
    {
        strafeAmount += 0.01f;
    }
    public void AccelerateStart()
    {
        pressAccelPedalSound.volume = 0.5f - currentSpeed / maxSpeed;
        pressAccelPedalSound.Play();
        acceling = true;
    }
    public void AccelerateEnd()
    {
        releaseAccelPedalSound.volume = 0.5f - currentSpeed / maxSpeed;
        releaseAccelPedalSound.Play();
        acceling = false;
    }
    public void BrakeStart()
    {
        pressBrakePedalSound.volume = 0.5f - currentSpeed / maxSpeed;
        pressBrakePedalSound.Play();
        braking = true;
    }
    public void BrakeEnd()
    {
        releaseBrakePedalSound.volume = 0.5f - currentSpeed / maxSpeed;
        releaseBrakePedalSound.Play();
        braking = false;
    }

    public void coast()
    {
        // Transform engineSounds = engineSound.transform; // Get Engine children
        // Debug.Log("Inside returnToNeutralSpeed");
        if (Mathf.Abs(neutralSpeed - currentSpeed) < 0.005f)
        {
            // Play Coasting Clip
            if (!engineSound.transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
            {
                // engineSound.transform.GetChild(0).GetComponent<AudioSource>().volume = 0.667f;
                // Debug.Log("Coasting Clip");
                engineSound.transform.GetChild(0).GetComponent<AudioSource>().Play(); // Play Coasting sound
            }

            // Blend from whatever to only Coasting
            BlendSnapshot(1, 0.5f);
            currentSpeed = neutralSpeed;
            //setRadioTempo(1f);
        }
        else if (currentSpeed > neutralSpeed)
        {
            // Blend form MaxSpeed to Coasting
            // Debug.Log("MaxSpeed->Coasting");
            BlendSnapshot(3, 0.5f);
            slowDown(0.001f);
        }
        else
        {
            // Blend from Rest to Coasting
            // Debug.Log("Rest->Coasting");
            BlendSnapshot(0, 1.5f);
            speedUp(0.1f);
        }
    }

    public void slowDown(float amount)
    {
        if (currentSpeed <= minSpeed) return;
        // Play Slowing Down Clip
        if (!engineSound.transform.GetChild(2).GetComponent<AudioSource>().isPlaying)
        {
            // Debug.Log("Slowing Clip");
            engineSound.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }

        // Blend from Coasting to Rest
        if (currentSpeed <= neutralSpeed)
        {
            // Debug.Log("Coasting->Rest");
            BlendSnapshot(4, 4f);
        }

        currentSpeed *= 1 - amount;
        //setRadioTempo(getRadioTempo()*(1-amount));
    }
    public void speedUp(float amount)
    {
        // Play Accelerating Clip
        if (!engineSound.transform.GetChild(1).GetComponent<AudioSource>().isPlaying)
        {
            // Debug.Log("Accel Clip");
            engineSound.transform.GetChild(1).GetComponent<AudioSource>().PlayScheduled(10.5f);
        }

        // Blend from Coasting to Max Speed
        if (currentSpeed > neutralSpeed)
        {
            // Debug.Log("Coasting->MaxSpeed");
            BlendSnapshot(2, 0.5f);
        }
        else
        {
            // Blend from Rest to Coasting
            // Debug.Log("Rest->Coasting");
            BlendSnapshot(0, 0.5f);
        }

        if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * amount;
            //setRadioTempo(getRadioTempo() + acceleration*amount/neutralSpeed);
        }
    }

    public void blockDirection(int direction)
    {
        blockedSide = direction;
    }

    public void strafe(float amount) //amount varies between -1 (steering wheel to the left) and 1 (steering wheel to the right)
    {
        panCarSounds(amount);

        //prevents car from moving if it's only nudged left/right
        //moves car to the side if there is no curb
        if (blockedSide / amount > 0)
        {
            if (amount < 0) wheelFunctions.PlaySideCollisionForce(-100);
            else if (amount > 0) wheelFunctions.PlaySideCollisionForce(100);
            //print("HITTING RAIL" + amount);
            if (lastRecordedStrafe == 0 || amount / lastRecordedStrafe <= 1f)
            {
                lastRecordedStrafe = amount;
            }
            return;
        }
        else if (Mathf.Abs(amount) > 0.01f)
        {
            transform.position += amount * (currentSpeed) * transform.right;
        }

        lastRecordedStrafe = amount;
    }

    private void panCarSounds(float amount)
    {
        engineSound.panStereo = amount * 3;
        foreach (Transform child in engineSound.gameObject.transform)
        {
            child.gameObject.GetComponent<AudioSource>().panStereo = amount * 3;
        }
        ConstructLevelFromMarkers.levelDialogue.panStereo = -amount * 1.5f;
        strafeSound.panStereo = amount * 2.5f;
    }

    private void OnDisable()
    {
        steerDirection = 0; //when does this fire? on collision?
    }

    private IEnumerator stopCarCoroutine()
    {
        enabled = false;
        while (currentSpeed > 0.01f)
        {
            lastRecordedStrafe *= 0.97f;
            currentSpeed *= 0.97f;

            rollSound.volume *= 0.97f;
            strafeSound.volume *= 0.98f;
            engineSound.volume *= 0.98f;
            foreach (Transform child in engineSound.gameObject.transform)
            {
                child.gameObject.GetComponent<AudioSource>().volume *= 0.98f;
            }
            yield return new WaitForFixedUpdate();
        }
        rollSound.volume = 0;
        strafeSound.volume = 0;
        lastRecordedStrafe = 0;
        currentSpeed = 0;
        engineSound.volume = 0;
        foreach (Transform child in engineSound.gameObject.transform)
        {
            child.gameObject.GetComponent<AudioSource>().volume = 0;
        }
    }

    public IEnumerator turnFailCoroutine(bool right)
    {
        disabledWheelSound.Play();
        float inc = -0.005f;
        if (right)
        {
            inc = 0.005f;
        }
        for (int i = 0; i < 5; i++)
        {
            lastRecordedStrafe += inc;
            yield return new WaitForFixedUpdate();
        }
        for (int i = 0; i < 5; i++)
        {
            lastRecordedStrafe -= inc;
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator impactCoroutine(Vector2 force)
    {
        this.enabled = false;
        print("diabled controls cause of collision");
        //int maxForce = 40;
        int totalIterations = 15;
        float maxHDisplacement = (force.x / 20);
        float maxVDisplacement = (Mathf.Pow(Mathf.Abs(force.y), 0.1f) * (force.y > 0 ? 1 : -1) / 2f);
        float origMovementSpeed = currentSpeed;
        float origStrafe = lastRecordedStrafe;
        for (int i = 0; i < totalIterations; i++)
        {
            Blur.setAmount(-Mathf.Pow((i - totalIterations), 2) / (totalIterations * (totalIterations / 1)) + 1);
            lastRecordedStrafe = -Mathf.Pow((i - totalIterations), 2)/(totalIterations * (totalIterations/maxHDisplacement)) + maxHDisplacement + origStrafe;
            currentSpeed = -Mathf.Pow((i - totalIterations), 2) / (totalIterations * (totalIterations / maxVDisplacement)) + maxVDisplacement + origMovementSpeed;
            yield return new WaitForFixedUpdate();
        }
        while (body.velocity.magnitude > 0.05f || lastRecordedStrafe > 0.02f)
        {
            body.velocity *= 0.975f;
            Blur.setAmount(Blur.getAmount() * 0.97f);
            lastRecordedStrafe *= 0.97f;
            currentSpeed *= 0.95f;
            yield return new WaitForFixedUpdate();
        }
        body.velocity *= 0;
        if (tag == "Player")
        {
            Blur.setAmount(0);
            lastRecordedStrafe = 0;
            currentSpeed = 0;
            body.bodyType = RigidbodyType2D.Kinematic;
            this.enabled = true;
        }
    }

    public IEnumerator shutOffCoroutine()
    {
        enabled = false;
        StartCoroutine(stopCarCoroutine());
        for (int i = 0; i < 200; i++)
        {
            shutOffSound.TransitionTo(0.5f);

            engineSound.volume += 0.01f;
            engineSound.pitch *= 0.99f;
            foreach (Transform child in engineSound.gameObject.transform)
            {
                child.gameObject.GetComponent<AudioSource>().volume += 0.01f;
                child.gameObject.GetComponent<AudioSource>().pitch *= 0.99f;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void parkCar()
    {
        StartCoroutine(stopCarCoroutine());
    }

    public float getStrafeAmount()
    {
        return lastRecordedStrafe;
    }

    public bool isAcceling()
    {
        return acceling;
    }

    public bool isBraking()
    {
        return braking;
    }

    // This function blends audio mixer snapshots together
    // Code was modified from the Unity Audio Mixer Snapshots YouTube tutorial:
    // https://youtu.be/2nYyws0qJOM
    public void BlendSnapshot(int transitionNum, float blendTime)
    {
        // Snapshot indices are as follows:
        // 0: Rest
        // 1: Rest to Coasting
        // 2: Coasting
        // 3: MaxSpeed
        // 4: MaxSpeed to Coasting
        if (engineMixer != null)
        {
            switch (transitionNum)
            {
                case 0: // Rest -> Coasting
                    snapshotWeights[0] = 0.0f;
                    snapshotWeights[1] = 1.0f;
                    snapshotWeights[2] = 0.0f;
                    snapshotWeights[3] = 0.0f;
                    snapshotWeights[4] = 0.0f;
                    engineMixer.TransitionToSnapshots(engineSounds, snapshotWeights, blendTime);
                    break;
                case 1: // Just Coasting
                    snapshotWeights[0] = 0.0f;
                    snapshotWeights[1] = 0.0f;
                    snapshotWeights[2] = 1.0f;
                    snapshotWeights[3] = 0.0f;
                    snapshotWeights[4] = 0.0f;
                    engineMixer.TransitionToSnapshots(engineSounds, snapshotWeights, blendTime);
                    break;
                case 2: // Coast -> Max Speed
                    snapshotWeights[0] = 0.0f;
                    snapshotWeights[1] = 0.0f;
                    snapshotWeights[2] = 0.0f;
                    snapshotWeights[3] = 1.0f;
                    snapshotWeights[4] = 0.0f;
                    engineMixer.TransitionToSnapshots(engineSounds, snapshotWeights, blendTime);
                    break;
                case 3: // Max Speed -> Coast
                    snapshotWeights[0] = 0.0f;
                    snapshotWeights[1] = 0.0f;
                    snapshotWeights[2] = 0.0f;
                    snapshotWeights[3] = 0.0f;
                    snapshotWeights[4] = 1.0f;
                    engineMixer.TransitionToSnapshots(engineSounds, snapshotWeights, blendTime);
                    break;
                case 4: // Coast -> Rest
                    snapshotWeights[0] = 1.0f;
                    snapshotWeights[1] = 0.0f;
                    snapshotWeights[2] = 0.0f;
                    snapshotWeights[3] = 0.0f;
                    snapshotWeights[4] = 0.0f;
                    engineMixer.TransitionToSnapshots(engineSounds, snapshotWeights, blendTime);
                    break;
            }
        }
    }
}
