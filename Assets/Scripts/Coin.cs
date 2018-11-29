using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    // When the player enters the collider of the coin, we send a score update and destroy the coin.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.SendMessage("AddToScore", 1);
            Destroy(gameObject);
        }
    }

}
