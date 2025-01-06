using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XP : MonoBehaviour
{
    // Variables for level, experience, and XP needed for the next level
    public int level = 1;
    [SerializeField] int maxlevel; 
    public float currentXP = 0f;              
    public float xpToNextLevel = 1000f;       
    public float xpGrowthRate = 1.1f;       

    private LevelupScreen screen;
    private bool levelUp;
    private UI ui;

    void Start()
    {
        screen = FindObjectOfType<LevelupScreen>(true);
        ui = FindObjectOfType<UI>();
        // Initialize the UI (if you are using it)
        UpdateUI();

    }

    // Method to gain XP
    public void GainXP(float amount)
    {
        currentXP += amount;

        // Check if XP exceeds or equals the required XP to level up
        if (currentXP >= xpToNextLevel && !levelUp && level <= maxlevel)
        {
            LevelUp();
        }

        // Update the UI
        UpdateUI();
    }

    // Level up the player and increase the XP needed for the next level
    private void LevelUp()
    {
        levelUp = true;
        Time.timeScale = 0f;
        screen.gameObject.SetActive(true);
        screen.LevelUP();
        level++;  // Increase the level
        currentXP -= xpToNextLevel;  // Deduct the XP needed for the level-up
        xpToNextLevel *= xpGrowthRate;  // Increase XP required by 3%

        Debug.Log("Level Up! New level: " + level);

        // Update the UI after leveling up
        UpdateUI();
        levelUp = false;
    }

    // Optional: Update the UI elements for level and XP
    private void UpdateUI()
    {
        if (ui.xpSlider != null) 
        {
            ui.xpSlider.maxValue = xpToNextLevel;
            ui.xpSlider.value = currentXP;
        }
    }
}
