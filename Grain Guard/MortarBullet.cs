using System.Collections;
using UnityEngine;

public class MortarBullet : Projectile
{
    public float arcHeight;
    public float flightDuration = 2.0f; // Fixed flight duration

    private Vector3 startPoint;
    private Vector3 lastKnownTargetPosition;
    private Transform target;

    private Collider2D col;

    [Header("Explosion")]
    [SerializeField] private float _ExpRadius;

    [SerializeField] private GameObject prefabWaterMelonExplosion;
    [SerializeField] private GameObject prefabCrater;

    public override void Start()
    {
        base.Start();

        col = GetComponent<Collider2D>();

        if (col != null)
        {
            col.enabled = false;
        }
    }

    public void Launch(Transform targetTransform, float projectileSpeed)
    {
        startPoint = transform.position;
        target = targetTransform;
        lastKnownTargetPosition = target.position; // Initialize with the target's current position
        StartCoroutine(MoveProjectile());
    }

    private IEnumerator MoveProjectile()
    {
        float startTime = Time.time;
        while (true)
        {
            float elapsedTime = Time.time - startTime;
            float fractionOfJourney = elapsedTime / flightDuration;

            if (target != null)
            {
                lastKnownTargetPosition = target.position;
            }

            Vector3 currentPosition = Vector3.Lerp(startPoint, lastKnownTargetPosition, fractionOfJourney);
            float height = Mathf.Sin(Mathf.PI * fractionOfJourney) * arcHeight;
            transform.position = new Vector3(currentPosition.x, currentPosition.y + height, currentPosition.z);

            if (fractionOfJourney >= 1f)
            {
                OnLanding();
                yield break;
            }

            yield return null;
        }
    }

    private void OnLanding()
    {
        if (col != null)
        {
            col.enabled = true;
        }
        if (target == null)
        {
            Vector3 craterPosition = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            Instantiate(prefabCrater, craterPosition, Quaternion.identity);
            Instantiate(prefabWaterMelonExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Explosion();
        }
        else
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _ExpRadius);
        foreach (Collider2D nearby in colliders)
        {
            EnemyHealth enemyHealth = nearby.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.SetShooter(_Shooter); // Set the shooter reference on the enemy
                enemyHealth.ChangeHealth(-_ProjectileDamage);
            }
        }
        Instantiate(prefabCrater, transform.position, Quaternion.identity);
        Instantiate(prefabWaterMelonExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _ExpRadius);
    }
}