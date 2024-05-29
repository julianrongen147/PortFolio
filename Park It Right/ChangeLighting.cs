using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChangeLighting : MonoBehaviour
{
    private Light myLight;
    private Color originalColor;

    public string lightGameObjectName = "DirectionalLight";

    void Start()
    {
        GameObject lightGameObject = GameObject.Find(lightGameObjectName);
        myLight = lightGameObject.GetComponent<Light>();

        originalColor = myLight.color;
    }

    public void ChangeLightToDark()
    {
        RenderSettings.ambientIntensity = 0f;
        myLight.color = Color.black;
    }

    public void ChangeLightToNormal()
    {
        RenderSettings.ambientIntensity = 1f;
        myLight.color = originalColor;
    }
}
