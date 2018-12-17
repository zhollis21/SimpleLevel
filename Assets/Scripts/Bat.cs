using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : DualDirectionEnemy
{

    private Actions currentAction;
    private Vector3 originPoint;

    private enum Actions { Sleeping, Flying }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        originPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Bats currently only support chase
        if (enemyMovementType == MovementPattern.Chase)
        {
            // Chasing the player
            if (playerTransform != null)
            {
                if (currentAction != Actions.Flying)
                {
                    enemyAnimator.SetBool("IsSleeping", false);
                    currentAction = Actions.Flying;
                }
                MoveTowards(playerTransform.position);
            }

            // Returning to our origin
            else if (transform.position != originPoint)
            {
                if (currentAction != Actions.Flying)
                {
                    enemyAnimator.SetBool("IsSleeping", false);
                    currentAction = Actions.Flying;
                }
                MoveTowards(originPoint);
            }

            else // Sleeping
            {
                if (currentAction != Actions.Sleeping)
                {
                    enemyAnimator.SetBool("IsSleeping", true);
                    currentAction = Actions.Sleeping;
                }
            }
        }
    }
}
