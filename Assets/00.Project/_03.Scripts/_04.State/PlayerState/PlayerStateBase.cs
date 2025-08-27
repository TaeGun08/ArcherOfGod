using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext
{
    public Player Player { get; set; }
    public PlayerController PlayerController { get; set; }
    public Animator Animator { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }
}

public abstract class PlayerStateBase : MonoBehaviour
{
    public Player Player { get; set; }
    public PlayerController PlayerController { get; set; }
    public Animator Animator { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }

    public virtual void Initialize(PlayerContext context)
    {
        Player = context.Player;
        PlayerController = context.PlayerController;
        Animator = context.Animator;
        Rigidbody2D = context.Rigidbody2D;
    }

    public abstract void StateEnter();
    public abstract void StateExit();
}
