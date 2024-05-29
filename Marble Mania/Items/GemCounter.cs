using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCounter : MonoBehaviour
{
    
    [HideInInspector] public int CurrentGems = 0;
    [SerializeField] public int MaxGems = 1;
    [SerializeField] private TMP_Text _GemText;

    private void Start()
    {
        _GemText.text = "Gems: " + CurrentGems.ToString() + "/" + MaxGems;
    }

    public void IncreaseGems(int amount)
    {
        CurrentGems += amount;
        _GemText.text = "Gems: " + CurrentGems.ToString() + "/" + MaxGems;
    }
}
