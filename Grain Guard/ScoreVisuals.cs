using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreVisuals : MonoBehaviour
{
    Punten punten;

    TextMeshProUGUI scoreText;


    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        punten = FindObjectOfType<Punten>();
    }

    private void Update()
    {
        scoreText.text = "" + punten.currentAmount;
    }
}
