using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasScaler : MonoBehaviour
{
    ScoreSystem scoreSystem;
    public RectTransform timerRect;

    private Image imageComponent;
    [SerializeField] private Image imageComponent2;

    [SerializeField] private Image timeBarBG;
    [SerializeField] private Image timeBar;

    float reductionAmountLeft = 30f;
    float reductionAmountRight = 10f;

    private void Start()
    {
        
    }

    public void ChangeUICanvasScale()
    {
        scoreSystem = FindObjectOfType<ScoreSystem>();
        imageComponent = GetComponent<Image>();

        float timerWidth = timerRect.rect.width;

        float parkingSpotWidth = scoreSystem.totalWidth;

        float totalWidth = timerWidth + parkingSpotWidth * scoreSystem.TotalParkingSpots;

        imageComponent.rectTransform.sizeDelta = new Vector2(totalWidth + 14, imageComponent.rectTransform.sizeDelta.y);

        imageComponent2.rectTransform.sizeDelta = new Vector2(totalWidth + 14, imageComponent2.rectTransform.sizeDelta.y);

        float newWidth = imageComponent2.rectTransform.sizeDelta.x - reductionAmountLeft - reductionAmountRight;

        timeBar.rectTransform.sizeDelta = new Vector2(newWidth, timeBar.rectTransform.sizeDelta.y);

        float newWidth2 = imageComponent2.rectTransform.sizeDelta.x - reductionAmountLeft - reductionAmountRight;

        timeBarBG.rectTransform.sizeDelta = new Vector2(newWidth, timeBarBG.rectTransform.sizeDelta.y);
    }
}
