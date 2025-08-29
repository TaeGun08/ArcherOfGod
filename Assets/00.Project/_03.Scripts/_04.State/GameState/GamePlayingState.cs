using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayingState : GameStateBase
{
    [Header("GamePlayingSettings")] [SerializeField]
    private GameObject[] backGrounds;

    [SerializeField] private SkillBase lightningSkill;
    [SerializeField] private GameObject SelectStat;

    public override void StateEnter()
    {
        GameManager.Instance.GameStarted = true;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        for (int i = GameManager.GamePlayingTime; i > 0; i--)
        {
            GameManager.TimeText.text = $"{i}";
            switch (i)
            {
                case 70:
                case 40:
                case 10:
                    Time.timeScale = 0f;
                    BotStatUpDown();
                    SelectStat.SetActive(true);
                    break;
            }


            while (Time.timeScale == 0f)
            {
                yield return null;
            }
            
            yield return wait;
        }

        GameManager.TimeText.text = $"Danger!!";
        backGrounds[0].SetActive(false);
        backGrounds[1].SetActive(true);
        yield return wait;
        lightningSkill.SkillEntry();
    }

    private void BotStatUpDown()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                GameManager.Instance.Bot.Stat.ShotSpeed += Random.Range(-0.4f, 0.4f);
                if (GameManager.Instance.Bot.Stat.ShotSpeed <= 0.1f)
                {
                    GameManager.Instance.Bot.Stat.ShotSpeed = 0.1f;
                }
                break;
            case 1:
                GameManager.Instance.Bot.Stat.MoveSpeed += Random.Range(-1f, 1f);
                if (GameManager.Instance.Bot.Stat.MoveSpeed <= 0.5f)
                {
                    GameManager.Instance.Bot.Stat.MoveSpeed = 0.5f;
                }
                break;
            case 2:
                GameManager.Instance.Bot.Stat.CurrentHp += Random.Range(-100, 101);
                if (GameManager.Instance.Bot.Stat.CurrentHp <= 0)
                {
                    GameManager.Instance.Bot.Stat.CurrentHp = 0;
                }

                if (GameManager.Instance.Bot.Stat.CurrentHp >= GameManager.Instance.Bot.Stat.MaxHp)
                {
                    GameManager.Instance.Bot.Stat.CurrentHp =  GameManager.Instance.Bot.Stat.MaxHp;
                }
                break;
        }
        
        GameManager.UpdateBotHp();
    }

    public override void StateExit()
    {
    }
}