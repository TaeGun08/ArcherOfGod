using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArrow : SkillBase
{
    private static readonly int Attack = Animator.StringToHash("Attack");

    [Header("SkillSettings")] 
    [SerializeField] private Arrow arrow;
    [SerializeField] private float attackDelay;
    [SerializeField] private int arrowCount;
    
    public override void SkillEntry()
    {
        skillCroutine = StartCoroutine(SkillCoroutine());
    }
    
    private IEnumerator SkillCoroutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(attackDelay);

        int count = arrowCount;
        while (count > 0)
        {
            animator.SetTrigger(Attack);
            
            yield return new WaitUntil(() =>
                animator.GetCurrentAnimatorStateInfo(0).IsName("attack"));
            
            yield return new WaitUntil(() =>
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f);
            
            Arrow(PoolObject(arrow.gameObject).GetComponent<Arrow>());
            
            yield return new WaitUntil(() =>
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

            yield return wfs;
            count--;
        }

        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.Player.PlayerController.ChangeState<PlayerAttackState>();
    }

    
    private void Arrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(true);
        Vector2 p0 = transform.position + (Vector3.up * 0.5f);
        Vector2 p1 = Vector2.up * 5f;
        Vector2 p2 = TargetRigidBody2D.transform.position;
        arrow.Duration = 1.5f;
        arrow.TargetPlayerOrBot = rigidbody2D.gameObject.layer.Equals(LayerMask.NameToLayer("Bot"));
        arrow.ShotArrow(p0, p1, p2);
    }

    public override void SkillEnd()
    {
        InputController.Instance.UseSkill = false;
        InputController.Instance.SkillCount = 0;
        skillCroutine = null;
    }
}
