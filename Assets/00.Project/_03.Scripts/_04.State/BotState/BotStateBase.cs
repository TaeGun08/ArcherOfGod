using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotContext
{
    public Bot Bot { get; set; }
    public BotController BotController { get; set; }
    public Animator Animator { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }
}

public abstract class BotStateBase : MonoBehaviour
{
    public Bot Bot { get; set; }
    public BotController BotController { get; set; }
    public Animator Animator { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }

    public virtual void Initialize(BotContext context)
    {
        Bot = context.Bot;
        BotController = context.BotController;
        Animator = context.Animator;
        Rigidbody2D = context.Rigidbody2D;
    }

    public abstract void StateEnter();
    public abstract void StateExit();
}
