using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text coinText;

    void Update()
    {
        scoreText.text = "Score : " + GameManager.Instance.score;
        coinText.text = "Coins : " + GameManager.Instance.coins;
    }
}
