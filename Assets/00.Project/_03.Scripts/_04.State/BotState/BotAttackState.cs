using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotAttackState : BotStateBase
{
    private AttackBase attackBase;
    private ArrowScanner arrowScanner;
    private Coroutine coroutine;

    public override void Initialize(BotContext context)
    {
        base.Initialize(context);
        attackBase = GetComponent<AttackBase>();
        attackBase.Initialize(context);
        arrowScanner = FindAnyObjectByType<ArrowScanner>();
    }

    public override void StateEnter()
    {
        attackBase.TargetRigidBody2D = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
        attackBase.AttackEntry(Stat.ShotSpeed);
        Vector3 scale = Rigidbody2D.transform.localScale;
        scale.x = 1f;
        Rigidbody2D.transform.localScale = scale;
        coroutine = StartCoroutine(RandomTimeCoroutine());
    }

    private IEnumerator RandomTimeCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
            case 1:
                BotController.ChangeState<BotWalkState>();
                break;
            default:
                Bot.TryUseSkill();
                break;
        }
    }

    public override void StateExit()
    {
        StopCoroutine(coroutine);
        attackBase.AttackEnd();
    }
}