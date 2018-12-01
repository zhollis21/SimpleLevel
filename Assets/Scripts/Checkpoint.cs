using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private bool hasBeenActivated;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !hasBeenActivated)
        {
            anim.SetBool("Activated", true);

            // Notice I'm passing in the players position as they trigger this
            // This is because the flag is bottom-left oriented and the player is center oriented
            // If I passed in the flag position the player would spawn strangely
            GameManager.instance.SetPlayerSpawnPoint(collision.transform.position);

            hasBeenActivated = true;
        }
    }
}
