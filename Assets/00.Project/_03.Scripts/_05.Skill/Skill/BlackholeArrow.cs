using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeArrow : SkillBase
{
    [Header("SkillSettings")] 
    [SerializeField] private Arrow arrow;
    
    protected override IEnumerator SkillCoroutine()
    {
        animator.SetTrigger(Attack);
        yield return null;

        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName("attack"));

        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f);

        Arrow(PoolObject(arrow.gameObject).GetComponent<Arrow>());

        yield return new WaitForSeconds(0.25f);
        GameManager.Instance.Player.PlayerController.ChangeState<PlayerAttackState>();
    }


    private void Arrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(true);
        Vector2 p0 = transform.position + (Vector3.up * 0.5f);
        Vector2 p1 = Vector2.up * 8f;
        Vector2 p2 = TargetRigidBody2D.transform.position;
        arrow.Duration = 1.5f;
        arrow.TargetPlayerOrBot = rigidbody2D.gameObject.layer.Equals(LayerMask.NameToLayer("Bot"));
        arrow.ShotArrow(p0, p1, p2);
    }
}
