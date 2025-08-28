using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public float MaxHp;
    public float CurrentHp;
    public float MoveSpeed;
    public float ShotSpeed;
}

public interface IStat
{
    public Stat Stat { get; set; }
}
