using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthbar;
    private Canvas canvas;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        health = GetComponent<Health>();
        healthbar.maxValue = health.maxhealth;
        healthbar.value = health.health;
    }

    public void UpdatehealthBar()
    {
        healthbar.value = health.health;
    }
}
