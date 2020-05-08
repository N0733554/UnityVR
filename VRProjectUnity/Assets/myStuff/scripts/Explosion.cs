using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float power = 10f;
    public float radius = 5f;
    public float upforce = 1f;
    
    private void Start()
    {
        Vector3 explosionPosition = transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb)
                rb.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.Impulse);
        }
    }
}
