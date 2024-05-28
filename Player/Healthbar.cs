using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    public static Healthbar instance;
    [SerializeField] public Image healthbar;
    PlayerHealth playerhealth;


    private void Start()
    {
        instance = this;
        healthbar = GetComponent<Image>();
        playerhealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        healthbar.fillAmount = (float)playerhealth.currentPlayerHealth / (float)playerhealth.maxPlayerHealth;
    }
}
