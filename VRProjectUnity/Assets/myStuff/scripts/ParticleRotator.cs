using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotator : MonoBehaviour
{
    public GameObject Object;
    
    public float RotationSpeed;

    void Update()
    {
        Object.transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
    }
}
