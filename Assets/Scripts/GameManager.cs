using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameOverUI gameOverUI;

    public int score;
    public int coins;

    public int coinMultiplier = 1;
    public int scoreMultiplier = 1;
    public int maxHealthLevel = 1;

    private float scoreTimer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadUpgrades();
        }
        else
        {
            Destroy(gameObject);
        }   
    }

    public float GetUpgradedMaxHealth()
    {
        float baseMaxHealth = 100f;
        float healthPerLevel = 20f;
        return baseMaxHealth + (maxHealthLevel - 1) * healthPerLevel;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PlayScene")
        {
            GameOverUI foundUI = FindObjectOfType<GameOverUI>();
            if (foundUI != null)
            {
                gameOverUI = foundUI;
            }
            else
            {
                Debug.LogWarning("GameOverUI not found in PlayScene!");
            }

            ResetSession();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= 1f)
            {
                score += 1 * scoreMultiplier;
                scoreTimer = 0;
            }
        }
    }

    public void CollectCoin(int amount)
    {
        coins += amount * coinMultiplier;
    }

    public void PlayerDied()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        PlayerPrefs.SetInt("TotalCoins", totalCoins + coins);
        PlayerPrefs.Save();

        Time.timeScale = 0f;
        gameOverUI.ShowGameOver(score);
    }

    public void Play()
    {
        SaveData();
        SceneManager.LoadScene("PlayScene");
    }

    void LoadUpgrades()
    {
        coinMultiplier = PlayerPrefs.GetInt("CoinMultiplier", 1);
        scoreMultiplier = PlayerPrefs.GetInt("ScoreMultiplier", 1);
        maxHealthLevel = PlayerPrefs.GetInt("MaxHealthLevel", 1);
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) + coins);
        PlayerPrefs.Save();
    }

    public void ResetSession()
    {
        score = 0;
        coins = 0;
        LoadUpgrades();
    }
}
