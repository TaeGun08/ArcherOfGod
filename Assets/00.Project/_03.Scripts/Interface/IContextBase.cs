using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContextBase
{
    public Stat Stat { get; set; }
    public Animator Animator { get; set; }
    public Rigidbody2D RigidBody2D { get; set; }
}
