using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScoreSystem : MonoBehaviour
{
    UICanvasScaler scaler;
    LevelTimer levelTimer;
    CarSpawn carSpawn;
    PlayerUIManager playerUIManager;

    public int TotalParkingSpots;
    [SerializeField] private Transform parkingSpots;

    [SerializeField] private GameObject parkingSpotImage;
    [SerializeField] private Transform parentObject;

    public float spacingImages;

    [SerializeField] private Sprite[] Images;
    private List<GameObject> parkingSpotObjects = new List<GameObject>();

    public int playerScore = 0;

    public float totalWidth;

    private void Start()
    {
        scaler = FindObjectOfType<UICanvasScaler>();
        levelTimer = GetComponent<LevelTimer>();
        carSpawn = FindObjectOfType<CarSpawn>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();

        foreach (Transform child in parkingSpots)
        {
            TotalParkingSpots += 1;
        }
        InstantiateImages();
        scaler.ChangeUICanvasScale();
    }

    private void Update()
    {
        if (playerScore >= TotalParkingSpots && levelTimer.extraTimeRemaining > 0)
        {
            LevelComplete();
        }

        if (playerScore < TotalParkingSpots && levelTimer.extraTimeRemaining <= 0)
        {
            LevelFailed();
        }
        if (levelTimer.normalTimer == 0 && carSpawn.canLevelFail)
        {
            LevelFailed();
        }
    }

    private void InstantiateImages()
    {
        float imageWidth = parkingSpotImage.GetComponent<RectTransform>().rect.width;
        float imageHeight = parkingSpotImage.GetComponent<RectTransform>().rect.height;

        totalWidth = (imageWidth + spacingImages);

        // Start position for the first image
        float startX = -totalWidth / 2f + imageWidth / 2f;

        for (int i = 0; i < TotalParkingSpots; i++)
        {
            float x = startX + (imageWidth + spacingImages) * i;
            float y = 0;

            GameObject newImage = Instantiate(parkingSpotImage, parentObject);

            RectTransform rt = newImage.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(imageWidth, imageHeight);
            rt.anchoredPosition = new Vector2(x, y);

            if (i < Images.Length && Images[i] != null)
            {
                newImage.GetComponent<Image>().sprite = Images[i];
            }

            // Add the new parking spot object to the list
            parkingSpotObjects.Add(newImage);
        }
    }

    public void ChangeImageColor(bool changeColorImage, int index)
    {
        if (changeColorImage && index >= 0 && index < parkingSpotObjects.Count)
        {
            Image imageComponent = parkingSpotObjects[index].GetComponent<Image>();
            if (imageComponent != null)
            {
                // Change color logic here, for example:
                imageComponent.color = Color.red;
            }
        }
    }

    private bool canSetWonMenuActive = false;

    private void LevelComplete()
    {
        StartCoroutine(LevelCompleteDelay());
        if (canSetWonMenuActive)
        {
            playerUIManager.wonMenu.SetActive(true);
        }
    }

    IEnumerator LevelCompleteDelay()
    {
        yield return new WaitForSeconds(1f);
        canSetWonMenuActive = true;
    }

    private void LevelFailed()
    {
        playerUIManager.loseMenu.SetActive(true);
    }
}
