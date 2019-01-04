using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMan : DualAxisMovementEnemy
{
    // Update is called once per frame
	void Update ()
    {
        if ((enemyMovementType == MovementPattern.Patrol || enemyMovementType == MovementPattern.PatrolAndChase) && patrolPoints.Count > 0)
        {
            Patrol();
        }
    }
}
