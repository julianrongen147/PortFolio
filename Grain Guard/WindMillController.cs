using UnityEngine;

public class WindMilController : MonoBehaviour
{
    public GameObject windEffectPrefab; // Prefab for the wind effect
    public Transform windSpawnPoint; // Point where the wind is spawned
    public float pushForce; // Force applied to push the units
    public float pushRange; // Range of the Air Sweeper
    public float pushAngle; // Angle of the cone in degrees
    public float basePushInterval; // Interval between each push
    private float pushInterval;

    private float lastPushTime;

    public bool drawWindMillGizmos;

    private TownhallUpgrade townhallUpgrade;

    private void Start()
    {
        townhallUpgrade = FindObjectOfType<TownhallUpgrade>();
    }

    void Update()
    {
        if (Time.time >= lastPushTime + pushInterval)
        {
            TryShootWind();
        }
    }

    void TryShootWind()
    {
        GameObject target = FindTargetInCone();

        if (target != null)
        {
            lastPushTime = Time.time;
            ShootWind(target);
        }
    }

    void ShootWind(GameObject target)
    {
        Vector2 directionToTarget = (target.transform.position - windSpawnPoint.position).normalized;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Check if the target is still within range and within the cone of vision
        if (Vector2.Distance(target.transform.position, windSpawnPoint.position) <= pushRange &&
            Mathf.Abs(Vector2.Angle(windSpawnPoint.right, directionToTarget)) < pushAngle / 2)
        {
            GameObject wind = Instantiate(windEffectPrefab, windSpawnPoint.position, Quaternion.Euler(0, 0, angle));
            WindEffect windEffect = wind.GetComponent<WindEffect>();
            if (windEffect != null)
            {
                windEffect.Initialize(pushForce, pushRange, pushAngle);
                CalculateCooldown();
            }
        }
    }

    GameObject FindTargetInCone()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, pushRange);
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hitCollider in hitColliders)
        {
            EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Vector2 directionToTarget = hitCollider.transform.position - transform.position;
                float distanceToTarget = directionToTarget.magnitude;

                // Check if the target is within the range of the windmill's vision
                if (distanceToTarget <= pushRange)
                {
                    // Check if the target is within the cone boundaries
                    Vector2 directionToTargetNormalized = directionToTarget.normalized;
                    float angleToTarget = Vector2.Angle(transform.right, directionToTargetNormalized);

                    // Check if the angle is within the cone angle
                    if (angleToTarget < pushAngle / 2)
                    {
                        // Calculate the dot product to determine if the target is within the cone boundaries
                        float dotProduct = Vector2.Dot(directionToTargetNormalized, transform.right);
                        if (dotProduct > Mathf.Cos(pushAngle / 2 * Mathf.Deg2Rad))
                        {
                            if (distanceToTarget < closestDistance)
                            {
                                closestEnemy = hitCollider.gameObject;
                                closestDistance = distanceToTarget;
                            }
                        }
                    }
                }
            }
        }

        return closestEnemy;
    }

    private void CalculateCooldown()
    {
        pushInterval = basePushInterval * townhallUpgrade.cooldownMultiplier;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 startPosition = windSpawnPoint.position;
        Vector3 direction = windSpawnPoint.right;

        // Draw the central line of the cone
        Gizmos.DrawLine(startPosition, startPosition + direction * pushRange);

        // Draw the left boundary of the cone
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -pushAngle / 2) * direction;
        Gizmos.DrawLine(startPosition, startPosition + leftBoundary * pushRange);

        // Draw the right boundary of the cone
        Vector3 rightBoundary = Quaternion.Euler(0, 0, pushAngle / 2) * direction;
        Gizmos.DrawLine(startPosition, startPosition + rightBoundary * pushRange);

        // Draw the arc of the cone
        int segments = 30;
        for (int i = 0; i < segments; i++)
        {
            float currentAngle = -pushAngle / 2 + (pushAngle / segments) * i;
            float nextAngle = -pushAngle / 2 + (pushAngle / segments) * (i + 1);

            Vector3 currentPoint = Quaternion.Euler(0, 0, currentAngle) * direction * pushRange;
            Vector3 nextPoint = Quaternion.Euler(0, 0, nextAngle) * direction * pushRange;

            Gizmos.DrawLine(startPosition + currentPoint, startPosition + nextPoint);
        }
    }
}
