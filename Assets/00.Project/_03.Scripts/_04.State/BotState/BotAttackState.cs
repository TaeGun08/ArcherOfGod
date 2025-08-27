using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttackState : BotStateBase
{
    private static readonly int Attack = Animator.StringToHash("Attack");

    [Header("AttackSettings")] [SerializeField]
    private float attackDelay;

    [SerializeField] private Arrow arrowPrefab;

    private Queue<Arrow> arrows = new Queue<Arrow>();

    private Coroutine attackCroutine;

    public override void StateEnter()
    {
        attackCroutine = StartCoroutine(ShotArrow());
    }

    private IEnumerator ShotArrow()
    {
        WaitForSeconds wfs = new WaitForSeconds(attackDelay);
        
        while (true)
        {
            Animator.SetTrigger(Attack);
            
            yield return null;
            
            while (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6f)
            {
                yield return null;
            }

            PoolArrow();

            yield return wfs;
        }
    }

    private void PoolArrow()
    {
        Arrow arrow = null;

        if (arrows.Count > 0)
        {
            for (int i = 0; i < arrows.Count; i++)
            {
                arrow = arrows.Dequeue();
                if (arrow.gameObject.activeSelf == false)
                {
                    Arrow(arrow);
                    arrows.Enqueue(arrow);
                    return;
                }

                arrows.Enqueue(arrow);
            }
        }

        arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, -180f), transform);
        Arrow(arrow);
        arrows.Enqueue(arrow);
        arrow.TargetPlayerOrBot = true;
    }

    private void Arrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(true);
        Vector2 p0 = BotController.transform.position + (Vector3.up * 0.5f);
        Vector2 p1 = Vector2.up * 8f;
        Vector2 p2 = GameManager.Instance.Player.transform.position;
        arrow.ShotArrow(p0, p1, p2, 0f);
    }

    private void Update()
    {
        if (InputController.Instance.Direction.magnitude > 0.01f)
        {
            BotController.ChangeState<BotWalkState>();
        }
    }

    public override void StateExit()
    {
        attackCroutine = null;
    }
}
