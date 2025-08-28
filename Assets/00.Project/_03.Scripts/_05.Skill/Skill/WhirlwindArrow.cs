using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindArrow : SkillBase
{
    [Header("Whirlwind Settings")]
    [SerializeField] private GameObject whirlwind;

    protected override IEnumerator SkillCoroutine()
    {
        animator.SetTrigger(Attack);
        PoolObject(whirlwind);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.Player.PlayerController.ChangeState<PlayerAttackState>();
    }
}
