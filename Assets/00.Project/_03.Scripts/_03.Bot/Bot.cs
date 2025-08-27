using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour, IDamageAble
{
    [field: Header("BotSettings")]
    [field: SerializeField] public float MaxHp { get; set; }
    [field: SerializeField] public float CurrentHp { get; set; }
    
    public void TakeDamage(float damage)
    {
        CurrentHp -= damage;

        if (CurrentHp > 0) return;
        GameManager.Instance.PlayerWin = true;
    }
}
