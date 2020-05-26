﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public AudioSource releasePedalSound;
    public AudioSource accelPedalSound;
    public AudioSource brakePedalSound;
    public AudioSource disabledWheelSound;
    public AudioSource engineSound;
    public AudioSource tireSound;
    public AudioSource dialogue;
    public AudioSource slidingSound;
    public AudioSource grabWheel;

    public float minSpeed = 0f;
    public float maxSpeed = 1.5f;
    public float neutralSpeed = 1f;
    public float acceleration = 0.01f;
    public float movementSpeed = 0f;

    private Rigidbody2D body;
    private AudioClip bump;
    private Vector3 movementDirection;
    private int blockedSide = 0;
    private float lastRecordedStrafe = 0;
    private int strafingDirection = -1;

    private bool impacted = false;
    private bool coasting = false;
    private bool acceling = false;
    private bool braking = false;
    private bool isTurning = false;

    public AudioSource strafeSound;

    public AudioMixer engineMixer;
    public AudioMixerSnapshot[] engineSounds;
    public AudioMixerSnapshot shutOffSound;

    [Header("Private Attributes (visible for debugging)")]
    [SerializeField] private float[] snapshotWeights;

    private SteeringWheelInput wheelFunctions;

    private string[] instruments = { "Lead", "Bass", "Keyboard", "Wind", "Support", "Drums" };

    void Start()
    {
        // engineSounds = engineSound.transform;
        body = GetComponent<Rigidbody2D>();
        bump = Resources.Load<AudioClip>("Audio/bumpend");
        movementDirection = transform.up;

        // AudioMixerSnapshot[] engineSounds = {restToCoast, coastToAccel};

        wheelFunctions = GetComponent<SteeringWheelInput>();
    }
    
    void FixedUpdate()
    {
        engineSound.volume = -Mathf.Pow((movementSpeed / maxSpeed), 2) + 1;
        tireSound.volume = Mathf.Pow((movementSpeed / maxSpeed), 2);
        strafeSound.volume = tireSound.volume / 2;
        //wheelFunctions.PlayDirtRoadForce((int)(Mathf.Pow((1-(movementSpeed/maxSpeed)),1) * 25));
        // Discrete turn l/r 
        transform.position += movementDirection * movementSpeed;
        //print(movementSpeed);
    }

    public void returnToNeutralSpeed()
    {
        if (acceling || braking)
        {
            releasePedalSound.volume = 0.5f-movementSpeed / maxSpeed;
            releasePedalSound.Play();
        }
        acceling = false;
        braking = false;
        if (!this.enabled) return;
        coasting = true;

        // Transform engineSounds = engineSound.transform; // Get Engine children
        // Debug.Log("Inside returnToNeutralSpeed");
        if (Mathf.Abs(neutralSpeed - movementSpeed) < 0.005f)
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
            movementSpeed = neutralSpeed;
            //setRadioTempo(1f);
        }
        else if (movementSpeed > neutralSpeed)
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
        coasting = false;
    }

    public void slowDown(float amount)
    {
        if (!coasting)
        {
            if (!braking)
            {
                brakePedalSound.volume = 0.5f-movementSpeed / maxSpeed;
                brakePedalSound.Play();
            }
            braking = true;
        }
        if (!this.enabled) return;
        if (movementSpeed <= minSpeed) return;
        // Play Slowing Down Clip
        if (!engineSound.transform.GetChild(2).GetComponent<AudioSource>().isPlaying)
        {
            // Debug.Log("Slowing Clip");
            engineSound.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }

        // Blend from Coasting to Rest
        if (movementSpeed <= neutralSpeed)
        {
            // Debug.Log("Coasting->Rest");
            BlendSnapshot(4, 4f);
        }

        movementSpeed *= 1 - amount;
        //setRadioTempo(getRadioTempo()*(1-amount));
    }
    public void speedUp(float amount)
    {
        if (!coasting)
        {
            if (!acceling)
            {
                accelPedalSound.volume = 0.5f-movementSpeed / maxSpeed;
                accelPedalSound.Play();
            }
            acceling = true;
        }
        if (!this.enabled) return;
        // Play Accelerating Clip
        if (!engineSound.transform.GetChild(1).GetComponent<AudioSource>().isPlaying)
        {
            // Debug.Log("Accel Clip");
            engineSound.transform.GetChild(1).GetComponent<AudioSource>().PlayScheduled(10.5f);
        }

        // Blend from Coasting to Max Speed
        if (movementSpeed > neutralSpeed)
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

        if (movementSpeed < maxSpeed)
        {
            movementSpeed += acceleration * amount;
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

        if (!this.enabled)
        {
            if (!isTurning && Mathf.Abs(amount) > 0)
            {
                StartCoroutine(turnFail(amount > 0));
            }
            return;
        }

        //check if the player has begun turning (may not work for gamepad)
        if (!isTurning && Mathf.Abs(amount) > Mathf.Abs(lastRecordedStrafe))
        {
            isTurning = true;
            if (!slidingSound.isPlaying)
            {
                slidingSound.Play();
            }
            slidingSound.panStereo = amount * 3f;
        }
        else if (isTurning && Mathf.Abs(amount) < Mathf.Abs(lastRecordedStrafe))
        {
            isTurning = false;
            if (slidingSound.isPlaying && Mathf.Abs(amount) > 0.02f)
            {
                grabWheel.Play();
            }
            slidingSound.Stop();
        }

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
            transform.position += amount * (movementSpeed) * transform.right;
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
        dialogue.panStereo = -amount * 3;
        strafeSound.panStereo = amount * 2;
    }

    public IEnumerator turnFail(bool right)
    {
        isTurning = true;
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
        isTurning = false;
    }

    public IEnumerator impact(Vector2 force)
    {
        this.impacted = true;
        this.enabled = false;
        print("diabled controls cause of collision");
        int maxForce = 40;
        int totalIterations = 15;
        float maxHDisplacement = (force.x / 20);
        float maxVDisplacement = (Mathf.Pow(Mathf.Abs(force.y), 0.1f) * (force.y > 0 ? 1 : -1) / 2f);
        float origMovementSpeed = movementSpeed;
        float origStrafe = lastRecordedStrafe;
        for (int i = 0; i < totalIterations; i++)
        {
            Blur.setAmount(-Mathf.Pow((i - totalIterations), 2) / (totalIterations * (totalIterations / 1)) + 1);
            lastRecordedStrafe = -Mathf.Pow((i - totalIterations), 2)/(totalIterations * (totalIterations/maxHDisplacement)) + maxHDisplacement + origStrafe;
            movementSpeed = -Mathf.Pow((i - totalIterations), 2) / (totalIterations * (totalIterations / maxVDisplacement)) + maxVDisplacement + origMovementSpeed;
            yield return new WaitForFixedUpdate();
        }
        while (body.velocity.magnitude > 0.1f)
        {
            Blur.setAmount(Blur.getAmount() * 0.98f);
            lastRecordedStrafe *= 0.98f;
            movementSpeed *= 0.95f;
            yield return new WaitForFixedUpdate();
        }
        body.velocity *= 0;
        if (tag == "Player")
        {
            Blur.setAmount(0);
            lastRecordedStrafe = 0;
            movementSpeed = 0;
            body.bodyType = RigidbodyType2D.Kinematic;
            this.enabled = true;
        }
    }

    private void OnDisable()
    {
        isTurning = false;
    }

    public IEnumerator shutOff()
    {
        impacted = false;
        enabled = false;
        StartCoroutine(stopCar());
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
        StartCoroutine(stopCar());
    }

    private IEnumerator stopCar()
    {
        impacted = false;
        enabled = false;
        while (movementSpeed > 0.01f)
        {
            lastRecordedStrafe *= 0.97f;
            movementSpeed *= 0.97f;

            tireSound.volume *= 0.97f;
            strafeSound.volume *= 0.98f;
            engineSound.volume *= 0.98f;
            foreach (Transform child in engineSound.gameObject.transform)
            {
                child.gameObject.GetComponent<AudioSource>().volume *= 0.98f;
            }
            yield return new WaitForFixedUpdate();
        }
        tireSound.volume = 0;
        strafeSound.volume = 0;
        lastRecordedStrafe = 0;
        movementSpeed = 0;
        engineSound.volume = 0;
        foreach (Transform child in engineSound.gameObject.transform)
        {
            child.gameObject.GetComponent<AudioSource>().volume = 0;
        }
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
