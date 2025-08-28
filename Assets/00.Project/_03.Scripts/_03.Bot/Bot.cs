using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour, IDamageAble, IStat
{
    private static readonly int Dead = Animator.StringToHash("Dead");
    
    [field: Header("BotSettings")]
    [field: SerializeField] public Stat Stat { get; set; }

    public BotController BotController { get; set; }

    private Animator animator;

    private void Awake()
    {
        BotController = GetComponent<BotController>();
        animator = GetComponentInChildren<Animator>();
    }
    
    public void TakeDamage(float damage)
    {
        if (Stat.CurrentHp <= 0) return;
        Stat.CurrentHp -= damage;
        GameManager.Instance.UpdateBotHp();
        
        if (Stat.CurrentHp > 0) return;
        BotController.ChangeState<BotDeadState>();
        animator.SetTrigger(Dead);
        GameManager.Instance.GameController.ChangeState<GameEndState>();
    }
}
