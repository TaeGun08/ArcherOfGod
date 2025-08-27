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
        GameManager.GameEndUI.SetActive(true);
        
        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync(0);
    }

    public override void StateExit()
    {

    }
}
