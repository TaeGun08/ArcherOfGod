using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext : IContextBase
{
    public Player Player { get; set; }
    public PlayerController PlayerController { get; set; }
    public Stat Stat { get; set; }
    public Animator Animator { get; set; }
    public Rigidbody2D RigidBody2D { get; set; }
}

public abstract class PlayerStateBase : MonoBehaviour
{
    public Player Player { get; set; }
    public PlayerController PlayerController { get; set; }
    public Stat Stat { get; set; }
    public Animator Animator { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }

    public virtual void Initialize(PlayerContext context)
    {
        Player = context.Player;
        PlayerController = context.PlayerController;
        Stat = context.Stat;
        Animator = context.Animator;
        Rigidbody2D = context.RigidBody2D;
    }

    public abstract void StateEnter();
    public abstract void StateExit();
}
