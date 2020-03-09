using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Wand : MonoBehaviour
{
    public GameObject wandHeadPoint;
    [HideInInspector]
    public SteamVR_Action_Boolean castAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

    private Transform wandHeadTransform;
    private Vector3 headPos;
    private Vector3 prevHeadPos;
    private Hand hand;

    private float wandSpeed = 0;

    private float wandCharge = 0;
    private float maxCharge = 100;

    [HideInInspector]
    public bool canFire = false;
    private bool isHeld = false;
    private bool isMoving = false;
    private bool isCharging = false;
    private bool isCharged = false;

    public ParticleSystem[] chargingSystems;
    public ParticleSystem[] chargedSystems;

    void Start()
    {
        stopParticles(chargingSystems);
        stopParticles(chargedSystems);
    }

    private void OnAttachedToHand(Hand attachedHand)
    {
        hand = attachedHand;
    }

    private void Update()
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
        canFire = true;
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

    public void CastSpell()
    {
        resetCharge();
        Debug.Log("Wand: " + transform.forward);

        canFire = false;
    }

    void resetCharge()
    {
        wandCharge = 0;
        isCharged = false;
        isCharging = true;
        stopParticles(chargedSystems);
        playParticles(chargingSystems);
    }

    public bool WasCastButtonReleased()
    {
        return castAction.GetStateUp(hand.handType);     
    }
}
