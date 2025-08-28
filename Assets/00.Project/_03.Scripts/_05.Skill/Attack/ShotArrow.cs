using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotArrow : AttackBase
{
    [Header("ArrowSettings")] [SerializeField]
    private Arrow arrowPrefab;

    private Queue<Arrow> arrows = new Queue<Arrow>();

    private Coroutine attackCroutine;

    public override void AttackEntry(float attackSpeed)
    {
        attackCroutine = StartCoroutine(ShotArrowCoroutine(attackSpeed));
    }

    public override void AttackEnd()
    {
        if (attackCroutine == null) return;
        StopCoroutine(attackCroutine);
        attackCroutine = null;
    }

    private IEnumerator ShotArrowCoroutine(float attackSpeed)
    {
        WaitForSeconds wfs = new WaitForSeconds(attackSpeed);

        while (true)
        {
            yield return wfs;
            
            animator.SetTrigger("Attack");

            yield return null;

            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6f)
            {
                yield return null;
            }

            PoolArrow();
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

        arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, -180f), GameManager.Instance.transform);
        Arrow(arrow);
        arrows.Enqueue(arrow);
    }

    private void Arrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(true);
        Vector2 p0 = transform.position + (Vector3.up * 0.5f);
        Vector2 p1 = Vector2.up * 8f;
        Vector2 p2 = TargetRigidBody2D.transform.position;
        arrow.TargetPlayerOrBot = rigidbody2D.gameObject.layer.Equals(LayerMask.NameToLayer("Bot"));
        arrow.ShotArrow(p0, p1, p2);
    }
}