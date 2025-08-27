using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotController : MonoBehaviour
{
    private BotStateBase currentState;
    private BotStateBase[] states;

    private void Awake()
    {
        var context = new BotContext()
        {
            Bot = GetComponent<Bot>(),
            BotController = this,
            Animator = GetComponentInChildren<Animator>(),
            Rigidbody2D = GetComponent<Rigidbody2D>(),
        };
        
        states = GetComponentsInChildren<BotStateBase>();

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
