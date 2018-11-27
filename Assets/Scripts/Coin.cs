using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    // When the player enters the collider of the coin, we destroy it.
    // ToDo: We will probably have this add to a score later
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.SendMessage("AddToScore", 1);
            Destroy(gameObject);
        }
    }

}
