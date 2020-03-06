using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wand : MonoBehaviour
{
    public GameObject wandHeadPoint;

    private Transform wandHeadTransform;
    private Vector3 headPos;
    private Vector3 prevHeadPos;

    float wandSpeed = 0;

    float wandCharge = 0;
    float maxCharge = 100;

    bool isHeld = false;
    bool isMoving = false;
    bool isCharging = false;
    bool isCharged = false;

    public ParticleSystem[] chargingSystems;
    public ParticleSystem[] chargedSystems;

    // Start is called before the first frame update
    void Start()
    {
        stopParticles(chargingSystems);
        stopParticles(chargedSystems);

    }

    // Update is called once per frame
    void Update()
    {
        wandHeadTransform = wandHeadPoint.transform;
        headPos = transform.TransformPoint(wandHeadTransform.position);

        wandSpeed = Vector3.Distance(headPos, prevHeadPos) / Time.deltaTime;

        isMoving = false;
        if (wandSpeed > 0)
            isMoving = true;
        
        if(isMoving && isHeld)
        {
            if(isCharging)
                increaseWandCharge();
        }

        prevHeadPos = headPos;
    }

    void increaseWandCharge()
    {
        int chargeAmount = (int)Mathf.Floor(wandSpeed)/10;
        wandCharge += chargeAmount;

        if (wandCharge >= maxCharge)
            wandCharged();
    }

    void wandCharged()
    {
        isCharged = true;
        isCharging = false;
        stopParticles(chargingSystems);
        playParticles(chargedSystems);
    }

    void playParticles(ParticleSystem[] particleSystems)
    {
        if(particleSystems.Length > 0)
        {
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Play();
            }
        }        
    }

    void stopParticles(ParticleSystem[] particleSystems)
    {
        if (particleSystems.Length > 0)
        {
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Stop();
            }
        }
    }

    public void startHolding()
    {
        isHeld = true;
        
        if(isCharged)
        {
            playParticles(chargedSystems);
        }
        else
        {
            isCharging = true;
            playParticles(chargingSystems);
        }

    }

    public void stopHolding()
    {
        isHeld = false;

        if (isCharged)
        {
            stopParticles(chargedSystems);
        }
        else
        {
            isCharging = false;
            stopParticles(chargingSystems);
        }
    }

    
}
