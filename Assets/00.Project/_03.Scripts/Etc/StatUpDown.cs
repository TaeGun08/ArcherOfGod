using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatUpDown : MonoBehaviour
{
    public enum EStatUpDown
    {
        AttackSpeed,
        MoveSpeed,
        Hp,
    }
    
    [Header("StatUpDownSettings")]
    [SerializeField] private EStatUpDown statUpDown;
    [SerializeField] private GameObject statSelectUI;
    
    public void UpDown()
    {
        float upDown = 0f;
        switch (statUpDown)
        {
            case EStatUpDown.AttackSpeed:
                upDown = Random.Range(-0.4f, 0.4f);
                GameManager.Instance.StatText.text = upDown >= 0 ? $"ShotDelay +{upDown.ToString("F2")}Up" 
                    : $"ShotDelay {upDown.ToString("F2")}Down";
                GameManager.Instance.Player.Stat.ShotSpeed += upDown;
                if (GameManager.Instance.Player.Stat.ShotSpeed <= 0.1f)
                {
                    GameManager.Instance.Player.Stat.ShotSpeed = 0.1f;
                }
                break;
            case EStatUpDown.MoveSpeed:
                upDown = Random.Range(-1f, 1f);
                GameManager.Instance.StatText.text = upDown >= 0 ? $"MoveSpeed +{upDown.ToString("F2")}Up" : $"MoveSpeed {upDown.ToString("F2")}Down";
                GameManager.Instance.Player.Stat.MoveSpeed += upDown;
                if (GameManager.Instance.Player.Stat.MoveSpeed <= 0.5f)
                {
                    GameManager.Instance.Player.Stat.MoveSpeed = 0.5f;
                }
                break;
            case EStatUpDown.Hp:
                upDown = Random.Range(-100, 101);
                GameManager.Instance.StatText.text = upDown >= 0 ? $"Hp +{upDown.ToString("F0")}Up" : $"Hp {upDown.ToString("F0")}Down";
                GameManager.Instance.Player.Stat.CurrentHp += upDown;
                if (GameManager.Instance.Player.Stat.CurrentHp <= 0)
                {
                    GameManager.Instance.Player.Stat.CurrentHp = 0;
                }

                if (GameManager.Instance.Player.Stat.CurrentHp >= GameManager.Instance.Player.Stat.MaxHp)
                {
                    GameManager.Instance.Player.Stat.CurrentHp =  GameManager.Instance.Player.Stat.MaxHp;
                }
                break;
        }
        
        GameManager.Instance.UpdatePlayerHp();
        Time.timeScale = 1f;
        GameManager.Instance.UpDownText();
        statSelectUI.SetActive(false);
    }
}
