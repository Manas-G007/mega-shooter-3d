using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;

    [SerializeField]
    private TextMeshProUGUI playerStatus;
    public override void Enter()
    {
    }
    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer=0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            if (moveTimer > Random.Range(3,7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 3));
                moveTimer = 0;
            }
            enemy.LastPos = enemy.Player.transform.position;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new SearchState());
            }
        }
    }
    public override void Exit()
    {
    }

    public void Shoot()
    {
        try
        {
            // refer to gun barrel (pos set)
            Transform gunBarrel = enemy.gunBarrel;
            // init new bullet
            GameObject bullet = GameObject.Instantiate(
                Resources.Load("Prefab/bullet") as GameObject,
                gunBarrel.position,
                enemy.transform.rotation);
            Vector3 shootDir = (enemy.Player.transform.position - gunBarrel.transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDir * 40;
            shotTimer = 0f;
        }
        catch 
        {

            playerStatus.text = "You Win";
            // Handle ExceptionType1
            Debug.Log("exception");
        }

    }
}
