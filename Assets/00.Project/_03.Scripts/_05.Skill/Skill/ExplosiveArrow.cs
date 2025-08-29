using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArrow : SkillBase
{
    [Header("SkillSettings")] 
    [SerializeField] private Arrow arrow;
    [SerializeField] private int arrowCount;
    
    protected override IEnumerator SkillCoroutine()
    {
        animator.SetTrigger(Attack);
        yield return null;

        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName("attack"));

        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f);

        for (int i = 0; i < arrowCount; i++)
        {
            Arrow(PoolObject(arrow.gameObject).GetComponent<Arrow>());
        }

        yield return new WaitForSeconds(0.25f);
        if (rigidbody2D.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.Player.PlayerController.ChangeState<PlayerAttackState>();
        }
        else
        {
            GameManager.Instance.Bot.BotController.ChangeState<BotAttackState>();
        }
    }


    private void Arrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(true);
        Vector2 p0 = transform.position + (Vector3.up * 0.5f);
        Vector2 p1 = Vector2.up * 6f;
        Vector2 p2 = TargetRigidBody2D.transform.position + new Vector3(Random.Range(-3f, 3f), 0f, 0f);
        arrow.Duration = 1.5f;
        arrow.TargetPlayerOrBot = rigidbody2D.gameObject.layer.Equals(LayerMask.NameToLayer("Bot"));
        arrow.ShotArrow(p0, p1, p2);
    }
}