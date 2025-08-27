using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    [field: Header("GameSettings")]
    [field: SerializeField] public bool GamePaused { get; set; }
    [field: SerializeField] public int GameStartTime { get; set; }
    [field: SerializeField] public int GamePlayingTime { get; set; }
    public bool GameStarted { get; set; }
    public bool GameEnded { get; set; }
    [field: Space]
    [field: Header("GameUI")]
    [field: SerializeField] public TMP_Text[] GameStartTextUI { get; set;}
    [field: SerializeField] public GameObject GameEndUI { get; set;}
}