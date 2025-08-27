using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext
{
    public GameManager GameManager { get; set; }
    public GameController GameController { get; set; }
}

public abstract class GameStateBase : MonoBehaviour
{
    protected GameManager GameManager { get; private set; }
    protected GameController GameController { get; private set; }

    public virtual void Initialize(GameContext context)
    {
        GameManager = context.GameManager;
        GameController = context.GameController;
    }

    public abstract void StateEnter();
    public abstract void StateExit();
}