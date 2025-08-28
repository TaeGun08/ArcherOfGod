using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageAble, IStat
{
    private static readonly int Dead = Animator.StringToHash("Dead");

    [field: Header("PlayerSettings")]
    [field: SerializeField] public Stat Stat { get; set; }
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(float damage)
    {
        Stat.CurrentHp -= damage;
        GameManager.Instance.UpdatePlayerHp();

        if (Stat.CurrentHp > 0) return;
        animator.SetTrigger(Dead);
        GameManager.Instance.PlayerWin = false;
    }
}
