using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public int coinID;
    private bool collected = false;

    // When the player enters the collider of the coin, we send a score update and destroy the coin.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected && collision.tag == "Player")
        {
            GameManager.instance.CoinCollected(coinID);
            collected = true;
            Destroy(gameObject);
        }
    }

}
