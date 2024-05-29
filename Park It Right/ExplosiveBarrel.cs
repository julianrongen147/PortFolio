using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] private float triggerForce = 0.5f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionForceHeight;
    [SerializeField] private float upwardsModifier;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= triggerForce)
        {
            var surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (var obj in surroundingObjects)
            {
                var rb = obj.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    continue;
                }

                explosionForce = Random.Range(50, 100);

                Vector3 explosionPos = transform.position;
                Vector3 direction = rb.transform.position - explosionPos;
                float distance = Vector3.Distance(rb.transform.position, explosionPos);
                float force = 1 - (distance / explosionRadius);

                Vector3 neutralizedVelocity = rb.velocity;

                Vector3 explosionForceVector = direction.normalized * explosionForce * force;
                explosionForceVector.y += upwardsModifier * explosionForce;
                explosionForceVector -= neutralizedVelocity;

                rb.AddForce(explosionForceVector ,ForceMode.Impulse);
            }

            Destroy(gameObject);
        }
    }
}
