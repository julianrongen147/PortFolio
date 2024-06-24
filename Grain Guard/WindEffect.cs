using UnityEngine;

public class WindEffect : MonoBehaviour
{
    private float pushForce;
    private float pushRange;
    private float pushAngle;

    private Vector3 originPosition;

    [SerializeField] private float destroyTimer = 5f;
    private float timer;

    public void Initialize(float force, float range, float angle)
    {
        pushForce = force;
        pushRange = range;
        pushAngle = angle;
    }

    private void Start()
    {
        originPosition = transform.position;
    }

    void Update()
    {
        // Move the wind effect forward
        transform.Translate(Vector2.right * Time.deltaTime * pushForce);

        timer += Time.deltaTime;
        if (timer >= destroyTimer)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Air Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            Rigidbody2D enemyRigidbody = collision.GetComponent<Rigidbody2D>();

            if (enemyHealth != null && enemyRigidbody != null)
            {
                // Apply force to the enemy while it stays in the trigger
                Vector2 pushDirection = transform.right * pushForce;
                enemyRigidbody.velocity = pushDirection;

            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Air Enemy"))
        {
            Rigidbody2D enemyRigidbody = collision.GetComponent<Rigidbody2D>();

            if (enemyRigidbody != null)
            {
                enemyRigidbody.velocity = Vector2.zero;
            }
        }
    }
}
