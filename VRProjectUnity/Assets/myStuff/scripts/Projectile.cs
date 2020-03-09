using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform explosionPrefab;
    private Rigidbody rb;

    public float explosionForce;
    public float explosionRadius;
    public float explosionUpforce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        if (collision.gameObject.layer != 8)
        {
            rb.AddExplosionForce(10, transform.position, 10, 1, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
