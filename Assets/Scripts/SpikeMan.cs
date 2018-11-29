using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMan : BaseEnemy
{

    private Actions currentAction;

    private enum Actions { Standing, Walking, Jumping }
    
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyMovementType == MovementPattern.Chase && playerTransform != null)
        {
            if (currentAction != Actions.Walking)
            {
                enemyAnimator.SetTrigger("Walk");
                currentAction = Actions.Walking;
            }

            enemyRenderer.flipX = playerTransform.position.x < transform.position.x;
            

            transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, playerTransform.position.x, movementSpeed * Time.deltaTime), transform.position.y);
            
        }
        else if (enemyMovementType == MovementPattern.Patrol)
        {

        }
        else if (currentAction != Actions.Standing)
        {
            enemyAnimator.SetTrigger("Stand");
            currentAction = Actions.Standing;
        }


    }
}
