using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStateBase currentState;
    private PlayerStateBase[] states;

    private void Awake()
    {
        var context = new PlayerContext
        {
            Player = GetComponent<Player>(),
            PlayerController = this,
            Animator = GetComponentInChildren<Animator>(),
            Rigidbody2D = GetComponent<Rigidbody2D>(),
        };
        
        states = GetComponentsInChildren<PlayerStateBase>();

        foreach (var state in states)
        {
            state.Initialize(context);
        }
        
        currentState = states[0];
        StartCoroutine(WaitStateEnter());
    }
    
    private IEnumerator WaitStateEnter()
    {
        yield return new WaitForSeconds(6f);
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
