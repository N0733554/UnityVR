using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manaBallMovement : MonoBehaviour
{
    private Transform sphere;

    public Wand wand;

    public float minSize;
    public float maxSize;
    public float scaleSpeed = 1f;

    public float bobAmount;
    public float bobSpeed = 1f;

    private float basePos = 0;

    void Start()
    {
        sphere = GetComponent<Transform>();
        basePos = transform.localPosition.x;
    }

    void Update()
    {
        // Calculate Oscillating Size
        float range = maxSize = minSize;
        float chargeAmount = wand.getWandCharge();
        chargeAmount *= range;
        chargeAmount /= 100;
        chargeAmount += minSize;

        // Calculate Bobbing Motion
        float translate = (Mathf.Sin(Time.time * bobSpeed));
        translate *= bobAmount;

        Vector3 scaleVec = new Vector3(chargeAmount, chargeAmount, chargeAmount);
        Vector3 moveVec = new Vector3(basePos + translate, transform.localPosition.y, transform.localPosition.z);

        transform.localScale = scaleVec;
        transform.localPosition = moveVec;
    }
}
