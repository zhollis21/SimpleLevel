using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public int value;

    // When the player enters the collider of the coin, we send a score update and destroy the coin.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.AddToScore(value);
            Destroy(gameObject);
        }
    }

}
