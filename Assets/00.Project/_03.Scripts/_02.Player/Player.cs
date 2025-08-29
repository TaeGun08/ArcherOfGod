using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageAble, IStat
{
    private static readonly int Dead = Animator.StringToHash("Dead");

    [field: Header("PlayerSettings")]
    [field: SerializeField] public Stat Stat { get; set; }

    public PlayerController PlayerController { get; set; }

    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        PlayerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(float damage)
    {
        if (Stat.CurrentHp <= 0) return;
        Stat.CurrentHp -= damage;
        GameManager.Instance.UpdatePlayerHp();

        if (Stat.CurrentHp > 0) return;
        PlayerController.ChangeState<PlayerDeadState>();
        animator.SetTrigger(Dead);
        GameManager.Instance.GameController.ChangeState<GameEndState>();
    }
}
