using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameStateBase currentState;
    private GameStateBase[] states;

    private void Awake()
    {
        var context = new GameContext()
        {
            GameManager = GetComponent<GameManager>(),
            GameController = this,
        };
        
        states = GetComponentsInChildren<GameStateBase>();

        foreach (var state in states)
        {
            state.Initialize(context);
        }
        
        currentState = states[0];
        currentState?.StateEnter();
    }

    public void ChangeState<T>()
    {
        if (enabled == false) return;
        
        currentState?.StateExit();
        currentState = states.FirstOrDefault(state => state is T);
        
        if (currentState == null) currentState = states[0];
        
        currentState?.StateEnter();
    }
}
