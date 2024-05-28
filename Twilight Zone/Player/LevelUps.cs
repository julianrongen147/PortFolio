using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUps : MonoBehaviour
{
    public static LevelUps instance;


    private void Start()
    {
        instance = this;
    }



    public void MovementLevel()
    {
        PlayerController.instance.speed += 25;
        Time.timeScale = 1f;
        Destroy(ScoreManager.instance.levelUpScreens);
    }

    public void FireRate()
    {
        GunHandler.instance.fireRate -= 0.02f;
        Time.timeScale = 1f;
        Destroy(ScoreManager.instance.levelUpScreens);
    }

    public void MaxHealthLevel()
    {
        PlayerHealth.instance.isMaxHealthLevelUp = true;
        PlayerHealth.instance.maxPlayerHealth += 10;
        PlayerHealth.instance.currentPlayerHealth += 10;
        Time.timeScale = 1f;
        Destroy(ScoreManager.instance.levelUpScreens);
    }

    public void HealthRegen()
    {
        PlayerHealth.instance.isHealthRegen = true;
        PlayerHealth.instance.currentPlayerHealth += 20;
        Time.timeScale = 1f;
        Destroy(ScoreManager.instance.levelUpScreens);
    }

    public void TorpedoCooldown()
    {
        GunHandler.instance.coolDownTorpedo -= 0.25f;
        Time.timeScale = 1f;
        Destroy(ScoreManager.instance.levelUpScreens);
    }
}
