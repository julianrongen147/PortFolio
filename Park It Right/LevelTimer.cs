using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    ChangeLighting changeLighting;
    MusicManager musicManager;

    [SerializeField] private TextMeshProUGUI timerText;
    public float normalTimer;

    private float flashInterval = 0.5f;
    private float flashTimer;

    [Tooltip("clockticking")]
    [SerializeField] private AudioSource clockTicking;
    [SerializeField] private AudioClip clockTickingClip;
    private bool canPlayClockTicking = true;

    [Tooltip("warningsound")]
    [SerializeField] private AudioSource warningSound;
    [SerializeField] private AudioClip warningSoundClip;
    private bool canPlayWarningSound = true;

    [SerializeField] private GameObject lightsCar;


    public Image timerBar;

    [HideInInspector] public float extraTimeRemaining;
    [SerializeField] private float maxExtraTimeRemaining;

    private void Start()
    {
        changeLighting = FindObjectOfType<ChangeLighting>();
        musicManager = FindObjectOfType<MusicManager>();

        lightsCar.gameObject.SetActive(false);

        extraTimeRemaining = maxExtraTimeRemaining;
    }

    private void Update()
    {
        if (normalTimer > 0)
        {
            PlayTimer();
        }

        if (normalTimer <= 11)
        {
            Timer10SecLeft();
        }

        if (normalTimer <= 1)
        {
            TimeIsUp();
        }

        int minutes = Mathf.FloorToInt(normalTimer / 60);
        int seconds = Mathf.FloorToInt(normalTimer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void PlayTimer()
    {
        normalTimer -= Time.deltaTime;
    }

    private void Timer10SecLeft()
    {
        musicManager.StopMusicNormal();

        if (canPlayClockTicking)
        {
            clockTicking.PlayOneShot(clockTickingClip);
            canPlayClockTicking = false;
        }
        timerText.color = Color.red;
        flashTimer += Time.deltaTime;

        if (flashTimer > flashInterval)
        {
            timerText.enabled = true;
            if (flashTimer >= 1)
            {
                flashTimer = 0;
            }
        }
        else if (flashTimer < flashInterval)
        {
            timerText.enabled = false;
        }
    }

    private void TimeIsUp()
    {
        normalTimer = 0;

        if (canPlayWarningSound)
        {
            warningSound.PlayOneShot(warningSoundClip);
            canPlayWarningSound = false;

            musicManager.PlayMusicNoTime();

            changeLighting.ChangeLightToDark();
            lightsCar.gameObject.SetActive(true);
        }

        musicManager.MusicNoTimeVolume();

        LastCarExtraTime();
    }

    private void LastCarExtraTime()
    {
        if (extraTimeRemaining > 0)
        {
            extraTimeRemaining -= Time.deltaTime;
            timerBar.fillAmount = extraTimeRemaining / maxExtraTimeRemaining;
        }
    }
}
