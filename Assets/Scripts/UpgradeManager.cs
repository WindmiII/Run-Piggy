using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public TMP_Text coinDisplay;

    public TMP_Text coinMultText;
    public TMP_Text scoreMultText;
    public TMP_Text maxHealthText;

    void Start()
    {
        UpdateUI();
    }

    public void UpgradeCoinMultiplier()
    {
        int level = PlayerPrefs.GetInt("CoinMultiplier", 1);
        int cost = 100 * level;
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        if (totalCoins >= cost)
        {
            PlayerPrefs.SetInt("TotalCoins", totalCoins - cost);
            PlayerPrefs.SetInt("CoinMultiplier", level + 1);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

    public void UpgradeScoreMultiplier()
    {
        int level = PlayerPrefs.GetInt("ScoreMultiplier", 1);
        int cost = 100 * level;
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        if (totalCoins >= cost)
        {
            PlayerPrefs.SetInt("TotalCoins", totalCoins - cost);
            PlayerPrefs.SetInt("ScoreMultiplier", level + 1);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

    public void UpgradeMaxHealth()
    {
        int level = PlayerPrefs.GetInt("MaxHealthLevel", 1);
        int cost = 150 * level;
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        if (totalCoins >= cost)
        {
            PlayerPrefs.SetInt("TotalCoins", totalCoins - cost);
            PlayerPrefs.SetInt("MaxHealthLevel", level + 1);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        int coinMult = PlayerPrefs.GetInt("CoinMultiplier", 1);
        int scoreMult = PlayerPrefs.GetInt("ScoreMultiplier", 1);
        int maxHealth = PlayerPrefs.GetInt("MaxHealthLevel", 1);

        int coinCost = 100 * coinMult;
        int scoreCost = 100 * scoreMult;
        int healthCost = 150 * maxHealth;

        coinDisplay.text = "Coins: " + PlayerPrefs.GetInt("TotalCoins", 0);
        coinMultText.text = "Coin Multiplier\n: x" + coinMult + "\nCost: " + coinCost;
        scoreMultText.text = "Score Multiplier\n: x" + scoreMult + "\nCost: " + scoreCost;
        maxHealthText.text = "Max Health\n: " + maxHealth + "\nCost: " + healthCost;
    }
}
