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
            GameManager.GameStartTextUI[0].text = $"{i}!!";
        }
        
        GameManager.GameStartTextUI[0].text = "Game Start!!";
        yield break;
    }

    public override void StateExit()
    {
    }
}
