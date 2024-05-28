using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    [SerializeField] public int maxPlayerHealth;
    [SerializeField] public int currentPlayerHealth;
    public bool isMaxHealthLevelUp = false;
    public bool isHealthRegen = false;
    public TextMeshProUGUI healthText;

    [Header("Damage Overlay")]
    [SerializeField] private Image hurt;
    [SerializeField] float duration;
    [SerializeField] float fadeSpeed;
    private float durationTimer;

    [Header("Lose screen")]
    [SerializeField] private GameObject loseScreen;

    private void Start()
    {
        Physics.IgnoreLayerCollision(6, 7, false);
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        hurt = GameObject.Find("Hurt").GetComponent<Image>();
        instance = this;
        currentPlayerHealth = maxPlayerHealth;
        hurt.color = new Color(hurt.color.r, hurt.color.g, hurt.color.b, 0);
        healthText.text = currentPlayerHealth + "/" + maxPlayerHealth.ToString();
    }

    void Update()
    {
        if (currentPlayerHealth > maxPlayerHealth)
        {
            currentPlayerHealth = maxPlayerHealth;
        }
        if (isMaxHealthLevelUp)
        {
            healthText.text = currentPlayerHealth + "/" + maxPlayerHealth.ToString();
        }
        if (isHealthRegen)
        {
            healthText.text = currentPlayerHealth + "/" + maxPlayerHealth.ToString();
        }

        if (hurt.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = hurt.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                hurt.color = new Color(hurt.color.r, hurt.color.g, hurt.color.b, tempAlpha);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION");
    }
    private void UpdateUI()
    {
        
    }


    public void PlayerTakeDamage(int amount)
    {
        IFrames iframes = GetComponent<IFrames>();
        if (iframes.IFramesSwitch == false)
        {
            currentPlayerHealth -= amount;
            UpdateUI();
            durationTimer = 0;
            hurt.color = new Color(hurt.color.r, hurt.color.g, hurt.color.b, 1);
            healthText.text = currentPlayerHealth + "/" + maxPlayerHealth.ToString();

            if (currentPlayerHealth <= 0)
            {
                Instantiate(loseScreen, GameObject.Find("Canvas").transform);
                Destroy(gameObject);
            }
        }
    }
}
