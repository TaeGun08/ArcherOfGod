using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntryState : GameStateBase
{
    public override void StateEnter()
    {
        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        for (int i = GameManager.GameStartTime; i > 0; i--)
        {
            GameManager.GameStartTextUI.text = $"{i}!!";
            yield return new WaitForSeconds(1f);
        }

        GameManager.GameStartTextUI.text = "Game Start!!";

        yield return new WaitForSeconds(0.5f);
        
        GameManager.GameStartUI.SetActive(false);
        GameController.ChangeState<GamePlayingState>();
    }

    public override void StateExit()
    {
    }
}