using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Armory : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Sprite[] lvls;
    [SerializeField] Sprite lockImg;
    [SerializeField] Sprite tickImg;
    [SerializeField] Image magnetImg;
    [SerializeField] Image shieldImg;
    [SerializeField] Image engineImg;
    [SerializeField] TextMeshProUGUI magnetCostTxt;
    [SerializeField] TextMeshProUGUI shieldCostTxt;
    [SerializeField] TextMeshProUGUI engineCostTxt;
    [SerializeField] Button magnetBtn;
    [SerializeField] Button shieldBtn;
    [SerializeField] Button engineBtn;
    [SerializeField] Image magnetBuyImg;
    [SerializeField] Image shieldBuyImg;
    [SerializeField] Image engineBuyImg;
    [SerializeField] TextMeshProUGUI coinsTxt;

    [Header("Upgrade Costs")]
    [SerializeField] int magnetCost;
    [SerializeField] int shieldCost;
    [SerializeField] int engineCost;
    [Range(0, 100)] public int percentageForUpgrade;

    private int currentmagnetCost;
    private int currentshieldCost;
    private int currentengineCost;

    private int magnetLvl = 0;
    private int shieldLvl = 0;
    private int engineLvl = 0;
    private int coins;

    private void Start()
    {
        currentmagnetCost = magnetCost;
        currentshieldCost = shieldCost;
        currentengineCost = engineCost;
        LoadStats();  
        magnetCostTxt.text = currentmagnetCost.ToString();
        shieldCostTxt.text = currentshieldCost.ToString();
        engineCostTxt.text = currentengineCost.ToString();

        coins = PlayerPrefs.GetInt("Coins");
        coinsTxt.text = "Your Coins: " + coins;

        UpdateUI(); 
    }

    private void Update()
    {
        coinsTxt.text = "Your Coins: " + coins;

        // Update buy button states based on available coins
        magnetBuyImg.sprite = currentmagnetCost <= coins ? tickImg : lockImg;
        shieldBuyImg.sprite = currentshieldCost <= coins ? tickImg : lockImg;
        engineBuyImg.sprite = currentengineCost <= coins ? tickImg : lockImg;
    }

    private void SaveStats()
    {
        PlayerPrefs.SetInt("Initialized", 1);
        PlayerPrefs.SetInt("currentMagnetCost", currentmagnetCost);
        PlayerPrefs.SetInt("magnetLvl", magnetLvl);
        PlayerPrefs.SetInt("currentShieldCost", currentshieldCost);
        PlayerPrefs.SetInt("shieldLvl", shieldLvl);
        PlayerPrefs.SetInt("currenEngineCost", currentengineCost);
        PlayerPrefs.SetInt("engineLvl", engineLvl);
        PlayerPrefs.Save();
    }

    private void LoadStats()
    {
        if (PlayerPrefs.HasKey("Initialized"))
        {
            currentmagnetCost = PlayerPrefs.GetInt("currentMagnetCost");
            magnetLvl = PlayerPrefs.GetInt("magnetLvl");
            currentshieldCost = PlayerPrefs.GetInt("currentShieldCost");
            shieldLvl = PlayerPrefs.GetInt("shieldLvl");
            currentengineCost = PlayerPrefs.GetInt("currentEngineCost");
            engineLvl = PlayerPrefs.GetInt("engineLvl");

            UpdateLvl(magnetImg, magnetLvl);
            UpdateLvl(shieldImg, shieldLvl);
            UpdateLvl(engineImg, engineLvl);

            UpdateCost();
        }     
    }

    private void UpdateLvl(Image img, int level)
    {
        img.sprite = lvls[level];
    }

    public void ResetSavings()
    {
        PlayerPrefs.DeleteAll();
    }

    private void UpdateCost()
    {
        // Adjust costs based on the levels
        currentmagnetCost = magnetCost + (magnetLvl * percentageForUpgrade);
        currentshieldCost = shieldCost + (shieldLvl * percentageForUpgrade);
        currentengineCost = engineCost + (engineLvl * percentageForUpgrade);

        // Update cost texts
        magnetCostTxt.text = currentmagnetCost.ToString();
        shieldCostTxt.text = currentshieldCost.ToString();
        engineCostTxt.text = currentengineCost.ToString();
    }

    private void UpdateUI()
    {
        // Update all UI elements to reflect the current data
        UpdateLvl(magnetImg, magnetLvl);
        UpdateLvl(shieldImg, shieldLvl);
        UpdateLvl(engineImg, engineLvl);
        magnetCostTxt.text = currentmagnetCost.ToString();
        shieldCostTxt.text = currentshieldCost.ToString();
        engineCostTxt.text = currentengineCost.ToString();
    }

    private void UpdateCoins(int amount)
    {
        PlayerPrefs.SetInt("Coins", coins);
    }

    public void MagnetBtn()
    {
        if(coins >= currentmagnetCost)
        {
            magnetLvl++;
            UpdateLvl(magnetImg, magnetLvl);
            coins -= currentmagnetCost;
            UpdateCoins(coins);

            PlayerPrefs.SetFloat("MagnetMultipliar", magnetLvl);

            currentmagnetCost = currentmagnetCost / 100 * (100 + percentageForUpgrade);
            magnetCostTxt.text = currentmagnetCost.ToString();

            if (magnetLvl >= 5) magnetBtn.gameObject.SetActive(false);
            SaveStats();
        }      
    }

    public void ShieldBtn()
    {
        if (coins >= currentshieldCost)
        {
            shieldLvl++;
            UpdateLvl(shieldImg, shieldLvl);
            coins -= currentshieldCost;
            UpdateCoins(coins);

            PlayerPrefs.SetFloat("ShieldMultipliar", 20 * shieldLvl);

            currentshieldCost = currentshieldCost / 100 * (100 + percentageForUpgrade);
            shieldCostTxt.text = currentshieldCost.ToString();

            if (shieldLvl >= 5) shieldBtn.gameObject.SetActive(false);
            SaveStats();
        }      
    }

    public void EngineBtn()
    {
        if (coins >= currentengineCost)
        {
            engineLvl++;
            UpdateLvl(engineImg, engineLvl);
            coins -= currentengineCost;
            UpdateCoins(coins);

            PlayerPrefs.SetFloat("EngineMultipliar", 1f - (engineLvl / 10f));

            currentengineCost = currentengineCost / 100 * (100 + percentageForUpgrade);
            engineCostTxt.text = currentengineCost.ToString();

            if (engineLvl >= 5) engineBtn.gameObject.SetActive(false);
            SaveStats();
        }         
    }
}
