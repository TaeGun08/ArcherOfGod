using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindSkill : SkillBase
{
    [Header("Whirlwind Settings")]
    [SerializeField] private GameObject whirlwind;
    [SerializeField] private bool whirlwindTarget;

    protected override IEnumerator SkillCoroutine()
    {
        animator.SetTrigger(Attack);
        GameObject whirlwindObj = PoolObject(whirlwind);
        whirlwindObj.GetComponent<Whirlwind>().WhirlwindTarget = whirlwindTarget;
        whirlwindObj.transform.position = new Vector3(transform.position.x, 1f, 0f);
        yield return new WaitForSeconds(0.5f);
        if (rigidbody2D.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.Player.PlayerController.ChangeState<PlayerAttackState>();
        }
        else
        {
            GameManager.Instance.Bot.BotController.ChangeState<BotAttackState>();
        }
    }
}
