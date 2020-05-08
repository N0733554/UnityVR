using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotator : MonoBehaviour
{
    public float RotationSpeed;
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
    }
}
