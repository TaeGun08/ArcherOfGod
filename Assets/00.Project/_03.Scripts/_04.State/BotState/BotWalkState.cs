using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotWalkState : BotStateBase
{
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private MovementBase movementBase;
    private ArrowScanner arrowScanner;

    public override void Initialize(BotContext context)
    {
        base.Initialize(context);
        movementBase = GetComponent<MovementBase>();
        movementBase.Initialize(context);
        arrowScanner = FindAnyObjectByType<ArrowScanner>();
    }

    public override void StateEnter()
    {
        Animator.SetTrigger(Walk);
        float dirX = Random.Range(0, 2) == 0 ? -1 : 1;
        arrowScanner.Direction = new Vector2(dirX, 0);
        StartCoroutine(RandomWalk());
    }

    private IEnumerator RandomWalk()
    {
        float waitTime = Random.Range(1f, 4f);
        float randomTime = Random.Range(0.5f, 2f);
        while (waitTime > 0)
        {
            waitTime -= Time.fixedDeltaTime;
            randomTime -= Time.fixedDeltaTime;
            
            if (randomTime <= 0)
            {
                float dirX = Random.Range(0, 2) == 0 ? -1 : 1;
                arrowScanner.Direction = new Vector2(dirX, 0);
                randomTime = Random.Range(0.5f, 2f);
            }

            yield return new WaitForFixedUpdate();
        }

        BotController.ChangeState<BotAttackState>();
    }

    private void FixedUpdate()
    {
        movementBase.Movement(Stat.MoveSpeed);
    }

    public override void StateExit()
    {
        Animator.SetTrigger(Idle);
    }
}