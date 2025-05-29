using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float runSpeed;
    public float jumpPower;
    private int jumpCount = 0;
    private bool canJump = true;
    Animator anim;
    public bool isGameOver = false;
    bool isSliding = false;

    [Header("Health System")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float healthDrainRate = 5f;
    public float obstacleDamage = 20f;
    public Slider healthBar;

    [Header("Damage & Invincibility")]
    public float invincibilityDuration = 1.5f;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine("IncreaseGameSpeed");

        maxHealth = GameManager.Instance.GetUpgradedMaxHealth();
        currentHealth = maxHealth;
        
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        GameManager.Instance.ResetSession();
    }

    void Update()
    {
        if (!isGameOver)
        {
            transform.position = Vector3.right * runSpeed * Time.deltaTime + transform.position;
        }

        if (jumpCount == 2)
        {
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isGameOver)
        {
            rb2d.velocity = Vector3.up * jumpPower;
            anim.SetTrigger("Jump");
            jumpCount += 1;
        }

        if (Input.GetKey(KeyCode.V) && canJump && !isGameOver)
        {
            StartSliding();
        }
        else
        {
            StopSliding();
        }

        DrainHealthOverTime();

    }

    void DrainHealthOverTime()
    {
        currentHealth -= healthDrainRate * Time.deltaTime;

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0 && !isGameOver)
        {
            GameOver();
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0 && !isGameOver)
        {
            GameOver();
        }
    }

    void StartSliding()
    {
        if (isSliding) return;

        isSliding = true;
        anim.SetBool("Slide", true);
    }

    void StopSliding()
    {
        if (!isSliding) return;

        isSliding = false;
        anim.SetBool("Slide", false);
    }

    public void GameOver()
    {
        isGameOver = true;
        anim.SetTrigger("Death");
        StopCoroutine("IncreaseGameSpeed");
        GameManager.Instance.PlayerDied();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            canJump = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            TakeDamage(obstacleDamage);
            StartCoroutine(InvincibilityFrames());

            Collider2D obstacleCollider = collision.collider;
            StartCoroutine(DisableColliderTemporarily(obstacleCollider));
        }
        else if (collision.gameObject.CompareTag("BottomDetector"))
        {
            GameOver();
        }
    }

    IEnumerator IncreaseGameSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            if (runSpeed < 12)
            {
                runSpeed += 0.2f;
            }
            if (GameObject.Find("GroundSpawner").GetComponent<ObstacleSpawner>().obstacleSpawnInterval > 1)
            {
                GameObject.Find("GroundSpawner").GetComponent<ObstacleSpawner>().obstacleSpawnInterval -= 0.1f;
            }
        }
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        float flashDuration = 0.1f;
        for (float i = 0; i < invincibilityDuration; i += flashDuration * 2)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDuration);
        }

        isInvincible = false;
    }

    IEnumerator DisableColliderTemporarily(Collider2D col)
    {
        col.enabled = false;
        yield return new WaitForSeconds(invincibilityDuration);
        col.enabled = true;
    }
    
}
