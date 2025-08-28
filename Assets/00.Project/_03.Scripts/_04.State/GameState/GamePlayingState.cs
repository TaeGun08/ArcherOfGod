using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayingState : GameStateBase
{
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
            yield return wait;
        }
    }

    public override void StateExit()
    {
    }
}
