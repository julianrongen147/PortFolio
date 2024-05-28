using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackDuration;

    [SerializeField] public float speed;
    [SerializeField] private float dashCooldown;
    private Rigidbody rb;
    public enum dashState
    {
        Ready,
        Using,
        Cooldown
    }
    private string[] dashStateNames = Enum.GetNames(typeof(dashState));
    public dashState d_state = dashState.Ready;

    public float dashSpeed;
    public float dashTime;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float hForce = h * speed * Time.deltaTime;
        float vForce = v * speed * Time.deltaTime;

        Vector3 force = new Vector3(hForce, 0, vForce);
        rb.AddForce(force, ForceMode.VelocityChange);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (d_state == dashState.Ready)
            {
                StartCoroutine(Dash());
            }
        }
    }

    IEnumerator Dash()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 && v == 0)
        {
            int direction = (h > 0) ? 1 : -1;
            rb.AddForce(new Vector3(direction * speed * dashSpeed * Time.deltaTime, 0, 0), ForceMode.VelocityChange);
        }
        if (h == 0 && v != 0)
        {
            int direction = (v > 0) ? 1 : -1;
            rb.AddForce(new Vector3(0, 0, direction * speed * dashSpeed * Time.deltaTime), ForceMode.VelocityChange);
        }
        if (h != 0 && v != 0)
        {
            int directionX = (h > 0) ? 1 : -1;
            int directionY = (v > 0) ? 1 : -1;
            rb.AddForce(new Vector3(directionX * speed * dashSpeed * Time.deltaTime, 0, directionY * speed * dashSpeed * Time.deltaTime), ForceMode.VelocityChange);
        }
        d_state = dashState.Cooldown;
        StartCoroutine(DashCooldown());
        yield return null;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        d_state = dashState.Ready;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb != null && collision.gameObject.layer == 7)
        {
            Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;
            StartCoroutine(Knockback(rb, knockbackDirection));
        }
    }

    private IEnumerator Knockback(Rigidbody rb, Vector3 knockbackDirection)
    {
        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
