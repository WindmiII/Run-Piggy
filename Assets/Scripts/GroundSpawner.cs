using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject Ground1, Ground2, Ground3;
    bool hasGround = true;

    void Update()
    {
        if (!hasGround)
        {
            SpawnGround();
            hasGround = true;
        }
    }

    public void SpawnGround()
    {
        int randomNum = Random.Range(1, 4);

        if (randomNum == 1)
        {
            Instantiate(Ground1, new Vector3(transform.position.x + 6, -8f, 0), Quaternion.identity);
        }
        else if (randomNum == 2)
        {
            Instantiate(Ground2, new Vector3(transform.position.x + 5, -7f, 0), Quaternion.identity);
        }
        else if (randomNum == 3)
        {
            Instantiate(Ground3, new Vector3(transform.position.x + 4, -6f, 0), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasGround = false;
        }
    }
}
