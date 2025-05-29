using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Coin, Score }
    public CollectibleType type;
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == CollectibleType.Coin)
                GameManager.Instance.CollectCoin(value);
            else if (type == CollectibleType.Score)
                GameManager.Instance.score += value * GameManager.Instance.scoreMultiplier;

            Destroy(gameObject);
        }
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
