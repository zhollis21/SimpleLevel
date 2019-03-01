using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [Tooltip("When enabled any trigger that touches a player will damage that player.")]
    public bool triggerCollidersDamage = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.SendMessage("Kill");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerCollidersDamage && other.tag == "Player")
        {
            other.SendMessage("Kill");
        }
    }
}