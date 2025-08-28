using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndState : GameStateBase
{
    public override void StateEnter()
    {
        StartCoroutine(GameEndCoroutine());
    }

    private IEnumerator GameEndCoroutine()
    {
        yield return new WaitForSeconds(1f);
        GameManager.GameEndUI.SetActive(true);

        if (GameManager.Player.Stat.CurrentHp <= 0)
        {
            GameManager.GameEndTextUI.text = "You Lose!";
        }

        if (GameManager.Bot.Stat.CurrentHp <= 0)
        {
            GameManager.GameEndTextUI.text = "You Win!";
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(0);
    }

    public override void StateExit()
    {

    }
}
