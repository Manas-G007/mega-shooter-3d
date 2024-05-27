using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int pointIndex;
    public float waitTime;
    public override void Enter()
    {
    }
    public override void Perform()
    {
        PatrolCycle();
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }
    public override void Exit()
    {
    }

    public void PatrolCycle()
    {
        waitTime += Time.deltaTime;
        if (waitTime < 3) return;
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            enemy.Agent.SetDestination(
                enemy.path.waypoints[pointIndex++%enemy.path.waypoints.Count].position);
        }
        waitTime = 0;
    }
}
